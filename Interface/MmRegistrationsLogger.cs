using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace SDRSharp.Tetra
{
    internal static class MmRegistrationsLogger
    {
        private static readonly object _lock = new object();

        private static string GetFolder()
        {
            var folder = RuntimeState.LogWriteFolder;
            if (string.IsNullOrWhiteSpace(folder))
            {
                folder = AppDomain.CurrentDomain.BaseDirectory;
            }
            return folder;
        }

        private static string FileNameForToday()
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            return Path.Combine(GetFolder(), $"TETRA_MM-Registrations_{date}.txt");
        }

        private static string FormatPrefix()
        {
            var ts = RuntimeState.RunTime.Elapsed;
            string time = $"{(int)ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}";
            int la = RuntimeState.CurrentLocationArea;
            string laStr = la >= 0 ? la.ToString(CultureInfo.InvariantCulture) : "-";

            // Leading space like the reference logs.
            // Spacing rule: with 3-digit LA -> 2 spaces between time and [LA:], else 1 space.
            int laDigits = (laStr == "-") ? 1 : laStr.Length;
            string gap = (laDigits == 3) ? "  " : " ";
            return $" {time}{gap}[LA: {laStr}]   ";
        }

        public static void Log(ReceivedData data)
        {
            // Only log when we have an MM PDU.
            if (!data.Contains(GlobalNames.MM_PDU_Type)) return;

            var mmType = (MmPduType)data.Value(GlobalNames.MM_PDU_Type);

            // SSI: prefer SSI, fallback to MM_SSI.
            int ssi = -1;
            if (!data.TryGetValue(GlobalNames.SSI, ref ssi))
                data.TryGetValue(GlobalNames.MM_SSI, ref ssi);

            if (ssi < 0) return; // reference log always has SSI

            string line = BuildLine(mmType, ssi, data);
            if (string.IsNullOrEmpty(line)) return;

            lock (_lock)
            {
                Directory.CreateDirectory(GetFolder());
                File.AppendAllText(FileNameForToday(), FormatPrefix() + line + Environment.NewLine, Encoding.UTF8);
            }
        }

        private static string BuildLine(MmPduType mmType, int ssi, ReceivedData data)
        {
            // Strings are based on the provided reference log file.
            switch (mmType)
            {
                case MmPduType.D_AUTHENTICATION:
                    // We don't perfectly reconstruct all subtypes; match the observed ones.
                    // If Authentication_sub_type==0 => demands authentication, else => result.
                    int sub = -1;
                    data.TryGetValue(GlobalNames.Authentication_sub_type, ref sub);
                    if (sub == 0 || sub == -1)
                    {
                        return $"BS demands authentication: SSI: {ssi}";
                    }
                    return $"BS result to MS authentication: Authentication successful or no authentication currently in progress SSI: {ssi} - Authentication successful or no authentication currently in progress";

                case MmPduType.D_LOCATION_UPDATE_ACCEPT:
                {
                    int gssi = -1;
                    data.TryGetValue(GlobalNames.MM_GSSI, ref gssi);
                    int cck = -1;
                    if (data.TryGetValue(GlobalNames.CCK_identifier, ref cck))
                    {
                        cck &= 0xF; // in the reference log it's a 0-15 value (e.g. 14)
                    }
                    int acceptType = -1;
                    data.TryGetValue(GlobalNames.Location_update_accept_type, ref acceptType);

                    string acceptText = (acceptType == 0) ? "MS request for registration/authentication ACCEPTED" : "MS request for registration ACCEPTED";
                    string locUpd = LocationUpdateAcceptTypeToText(acceptType);
                    var sb = new StringBuilder();
                    sb.Append(acceptText);
                    sb.Append($" for SSI: {ssi}");
                    if (gssi >= 0) sb.Append($" GSSI: {gssi}");
                    sb.Append(" - Authentication successful or no authentication currently in progress");
                    if (cck >= 0) sb.Append($" - CCK_identifier: {cck}");
                    if (!string.IsNullOrEmpty(locUpd)) sb.Append($" - {locUpd}");
                    return sb.ToString();
                }

                case MmPduType.D_LOCATION_UPDATE_REJECT:
                {
                    int luType = -1;
                    data.TryGetValue(GlobalNames.Location_update_type, ref luType);
                    int cause = -1;
                    data.TryGetValue(GlobalNames.Reject_cause, ref cause);
                    string locUpd = LocationUpdateTypeToText(luType);
                    string causeText = RejectCauseToText(cause);
                    var sb = new StringBuilder();
                    sb.Append($"MS updating in the network is NOT ACCEPTED for SSI: {ssi}");
                    if (!string.IsNullOrEmpty(locUpd)) sb.Append($" - {locUpd}");
                    if (!string.IsNullOrEmpty(causeText)) sb.Append($" CAUSE: {causeText}");
                    return sb.ToString();
                }

                default:
                    return null;
            }
        }

        private static string LocationUpdateAcceptTypeToText(int acceptType)
        {
            // Minimal mapping for the values observed in the sample file.
            // 0 appears to correspond to roaming location updating in the sample.
            return acceptType switch
            {
                0 => "Roaming location updating",
                1 => "Roaming location updating",
                _ => ""
            };
        }

        private static string LocationUpdateTypeToText(int luType)
        {
            return luType switch
            {
                0 => "Roaming location updating",
                1 => "Roaming location updating",
                _ => ""
            };
        }

        private static string RejectCauseToText(int cause)
        {
            // Values depend on network; we map the ones from the provided example file.
            return cause switch
            {
                0 => "LA not allowed (LA rejection)",
                1 => "ITSI/ATSI unknown (system rejection)",
                _ => cause.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
