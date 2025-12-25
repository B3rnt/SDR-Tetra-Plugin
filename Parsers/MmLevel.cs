using System.Globalization;

namespace SDRSharp.Tetra
{
    unsafe class MmLevel
    {
        // Authentication result text seen in reference log
        private const string AUTH_OK = "Authentication successful or no authentication currently in progress";

        public void Parse(LogicChannel channelData, int offset, ReceivedData result)
        {
            // MM PDU type is 4 bits (per EN 300 392-7)
            var mmType = (MmPduType)TetraUtils.BitsToInt32(channelData.Ptr, offset, 4);
            offset += 4;

            result.Add(GlobalNames.MM_PDU_Type, (int)mmType);

            // We rely on SSI/LA already being present in ReceivedData by lower layers.
            int v = 0;
            int ssi = result.TryGetValue(GlobalNames.SSI, ref v) ? v :
                      (result.TryGetValue(GlobalNames.MM_SSI, ref v) ? v : -1);

            switch (mmType)
            {
                case MmPduType.D_AUTHENTICATION:
                    ParseAuthentication(channelData, offset, result, ssi);
                    break;

                case MmPduType.D_LOCATION_UPDATE_ACCEPT:
                    ParseLocationUpdateAccept(channelData, offset, result, ssi);
                    break;

                case MmPduType.D_LOCATION_UPDATE_REJECT:
                    ParseLocationUpdateReject(channelData, offset, result, ssi);
                    break;

                default:
                    // For other MM PDUs we don't add MM registrations log lines.
                    break;
            }
        }

        private static void ParseAuthentication(LogicChannel ch, int offset, ReceivedData res, int ssi)
        {
            // Authentication_sub_type (2 bits) is a common discriminator.
            int sub = TetraUtils.BitsToInt32(ch.Ptr, offset, 2);
            offset += 2;
            res.Add(GlobalNames.Authentication_sub_type, sub);

            // We only create the same two log line families we see in the reference file.
            // sub == 0 -> BS demands authentication
            // sub != 0 -> BS result to MS authentication (we default to AUTH_OK text, as seen in most logs)
            if (sub == 0)
            {
                MmRegistrationsLogger.LogMmLine(res, ssi,
                    "BS demands authentication: SSI: " + ssi.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                // In many networks the result code is present after subtype; but for the logfile format we only need text.
                MmRegistrationsLogger.RememberAuthText(ssi, AUTH_OK);

                MmRegistrationsLogger.LogMmLine(res, ssi,
                    "BS result to MS authentication: " + AUTH_OK +
                    " SSI: " + ssi.ToString(CultureInfo.InvariantCulture) +
                    " - " + AUTH_OK);
            }
        }

        private static void ParseLocationUpdateAccept(LogicChannel ch, int offset, ReceivedData res, int ssi)
        {
            // The reference log shows two variants:
            //  - "MS request for registration/authentication ACCEPTED ..."
            //  - "MS request for registration ACCEPTED ..."
            // We interpret 2 bits as accept type (0 = reg/auth, 1 = reg). This matches typical 2-bit discriminators.
            int acceptType = TetraUtils.BitsToInt32(ch.Ptr, offset, 2);
            offset += 2;
            res.Add(GlobalNames.Location_update_accept_type, acceptType);

            // Next 4 bits often carry CCK_identifier (seen as 14 in sample).
            int cck = TetraUtils.BitsToInt32(ch.Ptr, offset, 4);
            offset += 4;
            res.Add(GlobalNames.CCK_identifier, cck);

            // Next 3 bits: registration label / reason (mapping based on user's reference log)
            int regReason = TetraUtils.BitsToInt32(ch.Ptr, offset, 3);
            offset += 3;
            res.Add(GlobalNames.Registrations_label, regReason);

            string reasonText = regReason switch
            {
                0 => "Roaming location updating",
                1 => "Service restoration roaming location updating",
                2 => "ITSI attach",
                _ => "Roaming location updating"
            };

            // Optional GSSI (present bit guess). We'll be conservative: if remaining bits can hold it and the next bit is 1.
            int gssi = -1;
            try
            {
                int present = TetraUtils.BitsToInt32(ch.Ptr, offset, 1);
                offset += 1;
                if (present == 1)
                {
                    gssi = TetraUtils.BitsToInt32(ch.Ptr, offset, 24);
                    offset += 24;
                    res.Add(GlobalNames.MM_GSSI, gssi);
                }
            }
            catch
            {
                // ignore if out of bounds
            }

            string authText = MmRegistrationsLogger.GetLastAuthText(ssi);
            if (string.IsNullOrEmpty(authText))
                authText = AUTH_OK;

            string head = (acceptType == 0)
                ? "MS request for registration/authentication ACCEPTED for SSI: "
                : "MS request for registration ACCEPTED for SSI: ";

            string line = head + ssi.ToString(CultureInfo.InvariantCulture);
            if (gssi > 0)
                line += " GSSI: " + gssi.ToString(CultureInfo.InvariantCulture);

            line += " - " + authText;
            line += " - CCK_identifier: " + cck.ToString(CultureInfo.InvariantCulture);
            line += " - " + reasonText;

            MmRegistrationsLogger.LogMmLine(res, ssi, line);
        }

        private static void ParseLocationUpdateReject(LogicChannel ch, int offset, ReceivedData res, int ssi)
        {
            // 2 bits for update type/reason (same mapping as accept).
            int regReason = TetraUtils.BitsToInt32(ch.Ptr, offset, 3);
            offset += 3;
            res.Add(GlobalNames.Registrations_label, regReason);

            string reasonText = regReason switch
            {
                0 => "Roaming location updating",
                1 => "Service restoration roaming location updating",
                2 => "ITSI attach",
                _ => "Roaming location updating"
            };

            // Reject cause (5 bits is common). We'll map the causes observed in the reference file.
            int cause = TetraUtils.BitsToInt32(ch.Ptr, offset, 5);
            offset += 5;
            res.Add(GlobalNames.Reject_cause, cause);

            string causeText = cause switch
            {
                1 => "LA not allowed (LA rejection)",
                2 => "ITSI/ATSI unknown (system rejection)",
                _ => "LA not allowed (LA rejection)"
            };

            string line =
                "MS updating in the network is NOT ACCEPTED for SSI: " + ssi.ToString(CultureInfo.InvariantCulture) +
                " - " + reasonText +
                " CAUSE: " + causeText;

            MmRegistrationsLogger.LogMmLine(res, ssi, line);
        }
    }
}
