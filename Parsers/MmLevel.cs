using System;
using System.Collections.Generic;
using System.Globalization;

namespace SDRSharp.Tetra
{
    /// <summary>
    /// Mobility Management decoder + MM registrations logfile writer.
    /// This is inspired by the rules-based MM parser found in the other source.
    /// Focus: reliably extract MM_SSI and key fields so the MM registrations logfile is produced.
    /// </summary>
    unsafe class MmLevel
    {
        // Rules copied/adapted from the other source (Class18.cs)
        private static readonly Rules[] RULES_LOCATION_UPDATE_ACCEPT = new Rules[]
        {
            new Rules(GlobalNames.Location_update_accept_type, 3, RulesType.Direct),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_SSI, 24, RulesType.Direct),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.Reserved, 24, RulesType.Reserved),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.Reserved, 16, RulesType.Reserved),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.CCK_identifier, 4, RulesType.Direct),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_GSSI, 24, RulesType.Direct)
        };

        private static readonly Rules[] RULES_LOCATION_UPDATE_REJECT = new Rules[]
        {
            new Rules(GlobalNames.Location_update_type, 3, RulesType.Direct),
            new Rules(GlobalNames.Reject_cause, 5, RulesType.Direct),
            new Rules(GlobalNames.Cipher_control, 1, RulesType.Direct),
            new Rules(GlobalNames.Ciphering_parameters, 10, RulesType.Switch, (int)GlobalNames.Cipher_control, 1, 0),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_Address_extension, 24, RulesType.Direct),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_SSI, 24, RulesType.Direct),
        };

        private const string AUTH_OK = "Authentication successful or no authentication currently in progress";

        public void Parse(LogicChannel channelData, int offset, ReceivedData result)
        {
            var mmType = (MmPduType)TetraUtils.BitsToInt32(channelData.Ptr, offset, 4);
            offset += 4;

            // Decode minimal/common items per message type.
            switch (mmType)
            {
                case MmPduType.D_LOCATION_UPDATE_ACCEPT:
                    ApplyRules(channelData, ref offset, result, RULES_LOCATION_UPDATE_ACCEPT);
                    EmitLog_LocationUpdateAccept(result);
                    break;

                case MmPduType.D_LOCATION_UPDATE_REJECT:
                    ApplyRules(channelData, ref offset, result, RULES_LOCATION_UPDATE_REJECT);
                    EmitLog_LocationUpdateReject(result);
                    break;

                case MmPduType.D_AUTHENTICATION:
                    ParseAuthentication(channelData, ref offset, result);
                    break;

                default:
                    // Still try to harvest MM_SSI from a common "options + presence + SSI" layout:
                    TryParseCommonSsi(channelData, ref offset, result);
                    break;
            }
        }

        private static void ApplyRules(LogicChannel ch, ref int offset, ReceivedData res, Rules[] rules)
        {
            bool presence = true;

            foreach (var r in rules)
            {
                switch (r.Type)
                {
                    case RulesType.Direct:
                        if (!presence)
                        {
                            // optional field absent -> don't consume bits
                            presence = true;
                            continue;
                        }
                        res.Add(r.GlobalName, TetraUtils.BitsToInt32(ch.Ptr, offset, r.Length));
                        offset += r.Length;
                        break;

                    case RulesType.Reserved:
                        if (!presence)
                        {
                            presence = true;
                            continue;
                        }
                        offset += r.Length;
                        break;

                    case RulesType.Options_bit:
                        // Always consume; store for completeness
                        res.Add(r.GlobalName, TetraUtils.BitsToInt32(ch.Ptr, offset, r.Length));
                        offset += r.Length;
                        break;

                    case RulesType.Presence_bit:
                        // Presence bit directly gates the NEXT rule.
                        presence = (TetraUtils.BitsToInt32(ch.Ptr, offset, 1) == 1);
                        res.Add(r.GlobalName, presence ? 1 : 0);
                        offset += 1;
                        break;

                    case RulesType.Switch:
                        // Switch based on a previously parsed field (Ext1 = GlobalNames as int, Ext2 = required value).
                        // If condition false, do NOT consume bits (same convention as presence bit).
                        int selectorName = r.Ext1;
                        int selectorValue = r.Ext2;
                        int current = -999;
                        if (selectorName >= 0 && selectorName < (int)GlobalNames.End)
                        {
                            res.TryGetValue((GlobalNames)selectorName, ref current);
                        }

                        if (current == selectorValue)
                        {
                            res.Add(r.GlobalName, TetraUtils.BitsToInt32(ch.Ptr, offset, r.Length));
                            offset += r.Length;
                        }
                        break;

                    default:
                        // not used in this MM subset
                        offset += r.Length;
                        break;
                }
            }
        }

        private static void ParseAuthentication(LogicChannel ch, ref int offset, ReceivedData res)
        {
            // In the other source, Authentication_sub_type is decoded here.
            int sub = TetraUtils.BitsToInt32(ch.Ptr, offset, 2);
            res.Add(GlobalNames.Authentication_sub_type, sub);
            offset += 2;

            // Try to harvest SSI using a common pattern (Options + Presence + SSI) or direct 24-bit SSI.
            TryParseCommonSsi(ch, ref offset, res);

            int ssi = GetSsi(res);

            // Emit log lines as seen in reference file.
            // subtype meaning differs by spec; we map minimally:
            // 0 -> BS demands authentication
            // 1 -> BS result to MS authentication (success)
            if (sub == 0)
            {
                MmRegistrationsLogger.LogMmLine(res, ssi,
                    "BS demands authentication: SSI: " + ssi.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                MmRegistrationsLogger.RememberAuthText(ssi, AUTH_OK);
                MmRegistrationsLogger.LogMmLine(res, ssi,
                    "BS result to MS authentication: " + AUTH_OK +
                    " SSI: " + ssi.ToString(CultureInfo.InvariantCulture) +
                    " - " + AUTH_OK);
            }
        }

        private static void TryParseCommonSsi(LogicChannel ch, ref int offset, ReceivedData res)
        {
            // Many MM PDUs contain an Options bit and one or more Presence bits gating a 24-bit SSI.
            // We'll try to parse without breaking alignment too much:
            // - If the next bits look like Options + Presence, use that.
            // - Otherwise, as a fallback, read 24 bits as MM_SSI if still missing.
            int tmp = 0;

            // If already present, don't re-parse.
            if (res.TryGetValue(GlobalNames.MM_SSI, ref tmp) && tmp > 0) return;

            int options = TetraUtils.BitsToInt32(ch.Ptr, offset, 1);
            int presence = TetraUtils.BitsToInt32(ch.Ptr, offset + 1, 1);

            if (options == 0 || options == 1)
            {
                // Consume options bit (we still store it)
                res.Add(GlobalNames.Options_bit, options);
                offset += 1;

                // Presence bit
                res.Add(GlobalNames.Presence_bit, presence);
                offset += 1;

                if (presence == 1)
                {
                    int ssi = TetraUtils.BitsToInt32(ch.Ptr, offset, 24);
                    res.Add(GlobalNames.MM_SSI, ssi);
                    offset += 24;
                    return;
                }
            }

            // Fallback: if we have enough bits left in the buffer, read 24 bits.
            // (Safer than producing no log at all.)
            int fallbackSsi = TetraUtils.BitsToInt32(ch.Ptr, offset, 24);
            if (fallbackSsi > 0)
            {
                res.Add(GlobalNames.MM_SSI, fallbackSsi);
            }
        }

        private static int GetSsi(ReceivedData res)
        {
            int v = -1;
            if (res.TryGetValue(GlobalNames.SSI, ref v) && v > 0) return v;
            if (res.TryGetValue(GlobalNames.MM_SSI, ref v) && v > 0) return v;
            return 0;
        }

        private static void EmitLog_LocationUpdateAccept(ReceivedData res)
        {
            int ssi = GetSsi(res);
            int gssi = -1;
            res.TryGetValue(GlobalNames.MM_GSSI, ref gssi);
            int cck = -1;
            res.TryGetValue(GlobalNames.CCK_identifier, ref cck);
            int acceptType = -1;
            res.TryGetValue(GlobalNames.Location_update_accept_type, ref acceptType);

            string proc = AcceptTypeToText(acceptType);

            string authText = MmRegistrationsLogger.GetLastAuthText(ssi);
            if (string.IsNullOrEmpty(authText)) authText = AUTH_OK;

            // Two very similar lines are seen in the reference file: registration/authentication ACCEPTED and registration ACCEPTED.
            // We write the combined one (most informative) and the registration one if a GSSI is present.
            string line1 = "MS request for registration/authentication ACCEPTED for SSI: " + ssi.ToString(CultureInfo.InvariantCulture);
            if (gssi > 0) line1 += " GSSI: " + gssi.ToString(CultureInfo.InvariantCulture);
            line1 += " - " + authText;
            if (cck >= 0) line1 += " - CCK_identifier: " + cck.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(proc)) line1 += " - " + proc;

            MmRegistrationsLogger.LogMmLine(res, ssi, line1);

            string line2 = "MS request for registration ACCEPTED for SSI: " + ssi.ToString(CultureInfo.InvariantCulture);
            if (gssi > 0) line2 += " GSSI: " + gssi.ToString(CultureInfo.InvariantCulture);
            line2 += " - " + authText;
            if (cck >= 0) line2 += " - CCK_identifier: " + cck.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(proc)) line2 += " - " + proc;

            MmRegistrationsLogger.LogMmLine(res, ssi, line2);
        }

        private static void EmitLog_LocationUpdateReject(ReceivedData res)
        {
            int ssi = GetSsi(res);
            int lut = -1;
            res.TryGetValue(GlobalNames.Location_update_type, ref lut);

            int cause = -1;
            res.TryGetValue(GlobalNames.Reject_cause, ref cause);

            string proc = LocationUpdateTypeToText(lut);
            string causeText = RejectCauseToText(cause);

            string line = "MS updating in the network is NOT ACCEPTED for SSI: " + ssi.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(proc)) line += " - " + proc;
            if (!string.IsNullOrEmpty(causeText)) line += " CAUSE: " + causeText;

            MmRegistrationsLogger.LogMmLine(res, ssi, line);
        }

        private static string AcceptTypeToText(int acceptType)
        {
            // Based on observed reference log phrases.
            return acceptType switch
            {
                0 => "Roaming location updating",
                1 => "Service restoration roaming location updating",
                2 => "ITSI attach",
                _ => "Roaming location updating"
            };
        }

        private static string LocationUpdateTypeToText(int t)
        {
            return t switch
            {
                0 => "Roaming location updating",
                1 => "Service restoration roaming location updating",
                2 => "ITSI attach",
                _ => "Roaming location updating"
            };
        }

        private static string RejectCauseToText(int cause)
        {
            // Minimal mapping for causes seen in the provided reference file.
            return cause switch
            {
                1 => "LA not allowed (LA rejection)",
                2 => "ITSI/ATSI unknown (system rejection)",
                _ => cause >= 0 ? ("Unknown (" + cause.ToString(CultureInfo.InvariantCulture) + ")") : "Unknown"
            };
        }
    }
}
