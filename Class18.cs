using System;
using System.Collections.Generic;
using System.Globalization;

namespace SDRSharp.Tetra
{
    /// <summary>
    /// Mobility Management (MM) parser/formatter.
    ///
    /// The reference project (SDRtetra.zip) ships an obfuscated Class18.
    /// We implement the same *observable* behavior for the MS Registrations/MM log:
    /// - same message templates
    /// - same location update type names
    /// - same reject cause names
    /// - OTAR subtype names
    /// - authentication result lines
    ///
    /// Parsing uses the same Rules layout that is visible in the reference source.
    /// </summary>
    internal static unsafe class Class18
    {
        // Cache last authentication result text per SSI to allow accept lines to include it.
        private static readonly Dictionary<int, string> _lastAuthTextBySsi = new Dictionary<int, string>();

        // Rules taken from the reference SDRtetra Class18 (non-obfuscated part).
        // Accept: Location_update_accept_type + presence bits for optional fields.
        private static readonly Rules[] _rulesLocationUpdateAccept =
        {
            new Rules(GlobalNames.Location_update_accept_type, 3, RulesType.Direct, 0, 0, 0),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0),

            // Optional: SSI
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
            new Rules(GlobalNames.MM_SSI, 24, RulesType.Direct, 0, 0, 0),

            // Optional: GSSI (in the reference this field is "Reserved 24" but it is printed as GSSI)
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
            new Rules(GlobalNames.MM_GSSI, 24, RulesType.Direct, 0, 0, 0),

            // Optional: CCK identifier (reference uses "Reserved 16", but is printed as CCK_identifier)
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
            new Rules(GlobalNames.CCK_identifier, 16, RulesType.Direct, 0, 0, 0),

            // Remaining optional/reserved blocks we ignore (keep offsets aligned)
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
            new Rules(GlobalNames.Reserved, 14, RulesType.Reserved, 0, 0, 0),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
            new Rules(GlobalNames.Reserved, 6, RulesType.Reserved, 0, 0, 0)
        };

        // Reject: Location_update_type + Reject_cause are first.
        private static readonly Rules[] _rulesLocationUpdateReject =
        {
            new Rules(GlobalNames.Location_update_type, 3, RulesType.Direct, 0, 0, 0),
            new Rules(GlobalNames.Reject_cause, 5, RulesType.Direct, 0, 0, 0),
            new Rules(GlobalNames.Cipher_control, 1, RulesType.Direct, 0, 0, 0),
            new Rules(GlobalNames.Ciphering_parameters, 10, RulesType.Switch, 389, 1, 0),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1, 0, 0),
            new Rules(GlobalNames.MM_Address_extension, 24, RulesType.Direct, 0, 0, 0)
        };

        // OTAR: subtype (4 bits) + optional SSI/address extension.
        private static readonly Rules[] _rulesOtarTail =
        {
            new Rules(GlobalNames.MM_SSI, 24, RulesType.Direct, 0, 0, 0),
            new Rules(GlobalNames.MM_Address_extension, 24, RulesType.Direct, 0, 0, 0),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit, 0, 0, 0)
        };

        public static MmInfo ParseMm(LogicChannel channelData, int offset, ReceivedData received)
        {
            var info = new MmInfo();

            // Provide LA/SSI from already-decoded globals if available.
            int la = received.Value(GlobalNames.Location_Area);
            if (la >= 0) info.LocationArea = la;

            int ssi = received.Value(GlobalNames.SSI);
            if (ssi >= 0) info.Ssi = ssi;

            // MM PDU type is 4 bits.
            int pduType = TetraUtils.BitsToInt32(channelData.Ptr, offset, 4);
            received.SetValue(GlobalNames.MM_PDU_Type, pduType);
            offset += 4;

            // Parse per PDU type.
            var tmp = new ReceivedData();
            tmp.SetValue(GlobalNames.MM_PDU_Type, pduType);

            switch ((MmPduType)pduType)
            {
                case MmPduType.D_OTAR:
                    {
                        int subType = SafeBits(channelData, offset, 4);
                        offset += 4;
                        tmp.SetValue(GlobalNames.Authentication_sub_type, subType); // reuse slot for subtype storage

                        // Tail rules: try to get SSI for logging
                        Global.ParseParams(channelData, offset, _rulesOtarTail, tmp);
                        int mmSsi = tmp.Value(GlobalNames.MM_SSI);
                        if (mmSsi > 0) info.Ssi = mmSsi;

                        info.Message = "OTAR: " + MmMaps.OtarSubTypeToText(subType) + " SSI: " + (info.Ssi ?? 0).ToString(CultureInfo.InvariantCulture);
                        return info;
                    }

                case MmPduType.D_AUTHENTICATION:
                    {
                        int subType = SafeBits(channelData, offset, 2);
                        offset += 2;
                        tmp.SetValue(GlobalNames.Authentication_sub_type, subType);

                        string authText;

                        // Heuristic: for Result PDUs, next 2 bits carry the result.
                        if ((D_AuthenticationPduSubType)subType == D_AuthenticationPduSubType.Result)
                        {
                            int res = SafeBits(channelData, offset, 2);
                            offset += 2;
                            authText = MmMaps.AuthenticationResultToText(res);

                            if (info.Ssi.HasValue && info.Ssi.Value > 0)
                            {
                                _lastAuthTextBySsi[info.Ssi.Value] = authText;
                            }

                            info.Message = "BS result to MS authentication: " + authText + " SSI: " + (info.Ssi ?? 0).ToString(CultureInfo.InvariantCulture)
                                         + " - " + authText;
                            return info;
                        }

                        if ((D_AuthenticationPduSubType)subType == D_AuthenticationPduSubType.Demand)
                        {
                            info.Message = "BS demands authentication: SSI: " + (info.Ssi ?? 0).ToString(CultureInfo.InvariantCulture);
                            return info;
                        }

                        // Other subtypes (Response/Reject) are less visible in the public logs; keep readable.
                        info.Message = "Authentication: " + ((D_AuthenticationPduSubType)subType).ToString();
                        return info;
                    }

                case MmPduType.D_LOCATION_UPDATE_ACCEPT:
                    {
                        Global.ParseParams(channelData, offset, _rulesLocationUpdateAccept, tmp);

                        int acceptType = tmp.Value(GlobalNames.Location_update_accept_type);
                        int mmSsi = tmp.Value(GlobalNames.MM_SSI);
                        int mmGssi = tmp.Value(GlobalNames.MM_GSSI);
                        int cck = tmp.Value(GlobalNames.CCK_identifier);

                        if (mmSsi > 0) info.Ssi = mmSsi;
                        if (mmGssi > 0) info.Gssi = mmGssi;
                        if (cck >= 0) info.CckIdentifier = cck;

                        string luText = MmMaps.LocationUpdateTypeToText(acceptType);
                        info.LuTypeText = luText;

                        string authText = string.Empty;
                        if (info.Ssi.HasValue && _lastAuthTextBySsi.TryGetValue(info.Ssi.Value, out var cached))
                        {
                            authText = cached;
                        }
                        if (string.IsNullOrEmpty(authText))
                        {
                            authText = "Authentication successful or no authentication currently in progress";
                        }
                        info.AuthText = authText;

                        string head = MmMaps.AcceptTypeToPrefix(acceptType);

                        // Compose exactly like the sample file:
                        // "MS request for registration/authentication ACCEPTED for SSI: X GSSI: Y - Authentication ... - CCK_identifier: 14 - Roaming location updating"
                        string msg = head + " for SSI: " + (info.Ssi ?? 0).ToString(CultureInfo.InvariantCulture);
                        if (info.Gssi.HasValue)
                        {
                            msg += " GSSI: " + info.Gssi.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        msg += " - " + authText;
                        if (info.CckIdentifier.HasValue)
                        {
                            msg += " - CCK_identifier: " + info.CckIdentifier.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        msg += " - " + luText;

                        info.Message = msg;
                        return info;
                    }

                case MmPduType.D_LOCATION_UPDATE_REJECT:
                    {
                        Global.ParseParams(channelData, offset, _rulesLocationUpdateReject, tmp);
                        int luType = tmp.Value(GlobalNames.Location_update_type);
                        int cause = tmp.Value(GlobalNames.Reject_cause);

                        string luText = MmMaps.LocationUpdateTypeToText(luType);
                        string causeText = MmMaps.RejectCauseToText(cause);

                        info.Message = "MS request for registration REJECTED for SSI: " + (info.Ssi ?? 0).ToString(CultureInfo.InvariantCulture)
                                     + " - " + luText + " CAUSE: " + causeText;
                        return info;
                    }

                case MmPduType.D_ATTACH_DETACH_GROUP_IDENTITY_ACKNOWLEDGEMENT:
                    {
                        // The reference UI prints this line when group identity attach/detach is acknowledged.
                        // If we have a GSSI in the already-decoded stream, include it.
                        int gssiVal = received.Value(GlobalNames.GSSI);
                        if (gssiVal > 0) info.Gssi = gssiVal;

                        info.Message = "BS acknowledges MS initiated attachment/detachment of group identities" +
                                       (info.Gssi.HasValue ? (" GSSI:" + info.Gssi.Value.ToString(CultureInfo.InvariantCulture)) : string.Empty) +
                                       " SSI: " + (info.Ssi ?? 0).ToString(CultureInfo.InvariantCulture) +
                                       " All attachment/detachments accepted";
                        return info;
                    }

                default:
                    info.Message = "MM: " + ((MmPduType)pduType).ToString();
                    return info;
            }
        }

        private static int SafeBits(LogicChannel ch, int offset, int len)
        {
            if (offset + len > ch.Length) return 0;
            return TetraUtils.BitsToInt32(ch.Ptr, offset, len);
        }
    }

    internal static class MmMaps
    {
        public static string AcceptTypeToPrefix(int acceptType)
        {
            // Observed in sample file:
            // - "MS request for registration/authentication ACCEPTED"
            // - "MS request for registration ACCEPTED"
            // Accept type is 3 bits. Keep mapping conservative.
            return acceptType == 0
                ? "MS request for registration/authentication ACCEPTED"
                : "MS request for registration ACCEPTED";
        }

        public static string LocationUpdateTypeToText(int t)
        {
            // Reference uses 3-bit location update type (0..7)
            // Based on the public MM examples + the shipped sample log.
            switch (t)
            {
                case 0: return "Roaming location updating";
                case 1: return "Migrating location updating";
                case 2: return "Periodic location updating";
                case 3: return "Demand location updating";
                case 4: return "Service restoration roaming location updating";
                case 5: return "Service restoration migrating location updating";
                case 6: return "ITSI attach";
                case 7: return "Disabled MS updating";
                default: return "Location updating";
            }
        }

        public static string RejectCauseToText(int c)
        {
            // Reject cause is 5 bits in the reference rules.
            // Names are taken from the public list you provided and match SDRtetra output style.
            switch (c)
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
                default: return "Cause " + c.ToString(CultureInfo.InvariantCulture);
            }
        }

        public static string AuthenticationResultToText(int res)
        {
            // The sample log uses only these two texts.
            return res == 1 ? "Authentication failed" : "Authentication successful or no authentication currently in progress";
        }

        public static string OtarSubTypeToText(int subType)
        {
            switch ((D_OtarPduSubType)subType)
            {
                case D_OtarPduSubType.CCK_Reject: return "CCK_Reject";
                case D_OtarPduSubType.SCK_Reject: return "SCK_Reject";
                case D_OtarPduSubType.GCK_Reject: return "GCK_Reject";
                case D_OtarPduSubType.GSKO_Reject: return "GSKO_Reject";
                case D_OtarPduSubType.DM_SCK_Activate: return "DM_SCK_Activate";
                case D_OtarPduSubType.CCK_Provide: return "CCK_Provide";
                case D_OtarPduSubType.SCK_Provide: return "SCK_Provide";
                case D_OtarPduSubType.GCK_Provide: return "GCK_Provide";
                case D_OtarPduSubType.GSKO_Provide: return "GSKO_Provide";
                default: return ((D_OtarPduSubType)subType).ToString();
            }
        }
    }
}
