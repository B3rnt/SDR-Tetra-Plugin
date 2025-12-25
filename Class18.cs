using System;

namespace SDRSharp.Tetra
{
    /// <summary>
    /// Clean (non-obfuscated) MM decoder/formatter.
    /// It focuses on producing the same MM registration log lines as the reference plugin.
    /// </summary>
    internal static class Class18
    {
        public static MmInfo ParseMm(LogicChannel channelData, int offset, ReceivedData received)
        {
            // offset points to MM PDU contents (right after MLE PDU type)
            var info = new MmInfo();

            // Location Area & SSI are available elsewhere in this plugin (decoded at lower layers)
            // Use those as primary sources to avoid relying on optional elements inside MM PDUs.
            int la = received.GetValue(GlobalNames.Location_Area);
            if (la >= 0) info.LocationArea = la;

            int ssi = received.GetValue(GlobalNames.SSI);
            if (ssi >= 0) info.Ssi = ssi;

            // MM PDU type is 4 bits at start of MM PDU
            int mmPduType = TetraUtils.BitsToInt32(channelData.Ptr, offset, 4);
            offset += 4;

            // Try to parse a few common fields used for the registrations log.
            // Many PDUs share: Location update type (2 bits) and reject cause (5 bits),
            // but layouts vary. We use conservative reads guarded by bounds.
            string msg = MmText.MapBasic(mmPduType);

            // Try parse auth subtype (2 bits) right after type for auth PDUs.
            if (mmPduType == 1 || mmPduType == 2) // heuristic: auth demand/result
            {
                int authSub = SafeBits(channelData, offset, 2);
                if (authSub >= 0)
                {
                    msg = MmText.MapAuthentication(mmPduType, authSub);
                    offset += 2;
                }
            }

            // Try parse location update accept/reject types
            if (mmPduType == 8) // accept (heuristic)
            {
                int accType = SafeBits(channelData, offset, 2);
                if (accType >= 0)
                {
                    msg = MmText.MapRegistrationAccepted(accType, info.Ssi);
                }
            }
            else if (mmPduType == 9) // reject (heuristic)
            {
                int luType = SafeBits(channelData, offset, 2);
                if (luType >= 0) offset += 2;
                int cause = SafeBits(channelData, offset, 5);
                if (cause >= 0)
                {
                    msg = MmText.MapRegistrationRejected(luType, cause, info.Ssi);
                }
            }

            info.Message = msg;
            return info;
        }

        private static int SafeBits(LogicChannel ch, int offset, int len)
        {
            if (offset + len > ch.Length) return -1;
            return TetraUtils.BitsToInt32(ch.Ptr, offset, len);
        }
    }

    internal static class MmText
    {
        public static string MapBasic(int pduType)
        {
            // Fallback
            return "MM: PDU " + pduType;
        }

        public static string MapAuthentication(int pduType, int subType)
        {
            // Reference log examples:
            // "BS demands authentication: SSI: x"
            // "BS result to MS authentication: Authentication successful or no authentication currently in progress SSI: x - Authentication successful or no authentication currently in progress"
            if (pduType == 1)
            {
                return "BS demands authentication";
            }
            if (pduType == 2)
            {
                // subtype 0 => successful/no auth in progress, 1 => failed (common)
                if (subType == 1)
                    return "BS result to MS authentication: Authentication failed";
                return "BS result to MS authentication: Authentication successful or no authentication currently in progress";
            }
            return "Authentication";
        }

        public static string MapRegistrationAccepted(int acceptType, int? ssi)
        {
            // Example:
            // "MS request for registration/authentication ACCEPTED for SSI: x"
            return "MS request for registration/authentication ACCEPTED";
        }

        public static string MapRegistrationRejected(int luType, int cause, int? ssi)
        {
            // Text list provided by user / reference blog
            // "MS request for registration REJECTED for SSI: x - Roaming location updating CAUSE: Illegal MS (system rejection)"
            return "MS request for registration REJECTED" + MapLuTypeSuffix(luType) + " CAUSE: " + MapRejectCause(cause);
        }

        private static string MapLuTypeSuffix(int luType)
        {
            switch (luType)
            {
                case 0: return " - Roaming location updating";
                case 1: return " - Periodic location updating";
                case 2: return " - Demand location updating";
                case 3: return " - Service restoration roaming location updating";
                default: return string.Empty;
            }
        }

        // Cause mapping (partial but extendable)
        public static string MapRejectCause(int cause)
        {
            switch (cause)
            {
                case 0: return "Illegal MS (system rejection)";
                case 1: return "Requested cipher key type not available (cell rejection)";
                case 2: return "Identified cipher key not available (cell rejection)";
                case 3: return "Roaming not supported (LA rejection)";
                case 4: return "LA unknown (LA rejection)";
                case 5: return "Identified cipher KSG not supported (cell rejection)";
                case 6: return "Use of DA cell not permitted (cell type rejection)";
                case 7: return "Ciphering required (cell rejection)";
                case 8: return "Migration not supported (system rejection)";
                case 9: return "Congestion (cell rejection)";
                case 10: return "Mandatory element error (system rejection)";
                case 11: return "No cipher KSG (cell rejection)";
                case 12: return "LA not allowed (LA rejection)";
                case 13: return "ITSI/ATSI unknown (system rejection)";
                case 14: return "Message consistency error (system rejection)";
                case 15: return "Forward registration failure (cell rejection)";
                case 16: return "Service not subscribed (LA rejection)";
                case 17: return "Network failure (cell rejection)";
                case 18: return "Use of CA cell not permitted (cell type rejection)";
                case 19: return "Authentication failure (system rejection)";
                default: return "Cause " + cause;
            }
        }
    }
}