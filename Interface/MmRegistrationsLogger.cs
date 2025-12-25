using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace SDRSharp.Tetra
{
    /// <summary>
    /// Writes a dedicated Mobility Management (MM) registrations/authentication logfile.
    /// Format is based on the user's provided reference file:
    ///   TETRA_MM-Registrations_YYYY-MM-DD.txt
    /// and line format:
    ///  HH:MM:SS[space][LA: ####]   <message>
    /// with a leading space before time and variable spacing before [LA: ].
    /// </summary>
    internal static class MmRegistrationsLogger
    {
        private static readonly object _lock = new object();

        // Relative time like 00:00:58
        private static readonly Stopwatch _sw = Stopwatch.StartNew();

        // Remember the last auth result text per SSI, because accept lines include it.
        private static readonly Dictionary<int, string> _lastAuthBySsi = new Dictionary<int, string>();

        private static string GetFilePath()
        {
            string folder = "";
            try { folder = Global.LogWriteFolder; } catch { }

            if (string.IsNullOrEmpty(folder))
            {
                try { folder = AppDomain.CurrentDomain.BaseDirectory; } catch { folder = "."; }
            }

            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
            }
            catch
            {
                // ignore, will fail later on write if invalid
            }

            var date = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var file = $"TETRA_MM-Registrations_{date}.txt";
            return Path.Combine(folder, file);
        }

        private static string ElapsedHms()
        {
            var ts = _sw.Elapsed;
            // match HH:MM:SS (hours can exceed 24; sample stays within)
            int hours = (int)ts.TotalHours;
            return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}", hours, ts.Minutes, ts.Seconds);
        }

        private static string FormatPrefix(int la)
        {
            // User note: "2 spaties is alleen bij 3 cijferige LA, bij 4 cijferige LA of meer maar 1 spatie"
            // i.e., after time: 2 spaces if LA has 3 digits; else 1 space.
            int digits = (la >= 0) ? la.ToString(CultureInfo.InvariantCulture).Length : 1;
            string gapAfterTime = (digits == 3) ? "  " : " ";
            return " " + ElapsedHms() + gapAfterTime + "[LA: " + la.ToString(CultureInfo.InvariantCulture) + "]   ";
        }

        private static int GetInt(ReceivedData data, GlobalNames name, int fallback = -1)
        {
            int v = 0;
            return data.TryGetValue(name, ref v) ? v : fallback;
        }

        private static int GetSsi(ReceivedData data)
        {
            int v = 0;
            if (data.TryGetValue(GlobalNames.MM_SSI, ref v)) return v;
            if (data.TryGetValue(GlobalNames.SSI, ref v)) return v;
            return -1;
        }

        private static int GetLa(ReceivedData data)
        {
            int v = 0;
            if (data.TryGetValue(GlobalNames.Location_Area, ref v)) return v;
            // fall back: some decoders use LAC / Location_area etc. Not in this codebase.
            return -1;
        }

        private static void AppendLine(string line)
        {
            var path = GetFilePath();
            lock (_lock)
            {
                File.AppendAllText(path, line + Environment.NewLine);
            }
        }

        // Public entry: write a fully formatted MM line.
        public static void LogMmLine(ReceivedData data, int ssi, string message)
        {
            if (!Global.LogMmRegistrations) return;

            int la = GetLa(data);
            if (la < 0) la = 0;

            // Ensure SSI is always present in the message like the reference file.
            // Callers should include it where required; but we keep this here safe.
            var prefix = FormatPrefix(la);
            AppendLine(prefix + message);
        }

        public static void RememberAuthText(int ssi, string authText)
        {
            if (ssi <= 0) return;
            lock (_lock)
            {
                _lastAuthBySsi[ssi] = authText ?? string.Empty;
            }
        }

        public static string GetLastAuthText(int ssi)
        {
            lock (_lock)
            {
                if (ssi > 0 && _lastAuthBySsi.TryGetValue(ssi, out var t)) return t;
            }
            return string.Empty;
        }
    }
}
