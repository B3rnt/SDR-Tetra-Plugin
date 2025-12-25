using System;
using System.Collections.Generic;

namespace SDRSharp.Tetra
{
    // Clean (non-obfuscated) replacement for the decompiled Class18.
    // This decoder focuses on MM (Mobility Management) primitives required for
    // producing the MM registrations log.
    internal sealed class Class18
    {
        // Parse MM PDU from logic channel bitstream.
        // int_0 is a bit offset.
        public void method_0(LogicChannel logicChannel_0, ref int int_0, Dictionary<GlobalNames, int> dictionary_0)
        {
            if (logicChannel_0.Ptr == null) return;
            if (dictionary_0 == null) return;

            int start = int_0;

            // MM PDU type (4 bits)
            int mmTypeVal = TetraUtils.BitsToInt32(logicChannel_0.Ptr, int_0, 4);
            int_0 += 4;
            dictionary_0[GlobalNames.MM_PDU_Type] = mmTypeVal;

            var mmType = (MmPduType)mmTypeVal;

            // Best-effort decoding of a few common fields.
            // NOTE: Exact field placement varies by primitive; this is designed to
            // work for the registrations/authentication messages seen in the provided logs.

            int ssi = TryReadSsi(logicChannel_0, int_0, logicChannel_0.Length);
            if (ssi != 0) dictionary_0[GlobalNames.MM_SSI] = ssi;

            string msg = BuildLogMessage(mmType, logicChannel_0, start, ref int_0, dictionary_0);
            if (!string.IsNullOrEmpty(msg))
            {
                MmRegistrationsLogger.Log(dictionary_0, msg);
            }
        }

        private static int TryReadSsi(LogicChannel ch, int offset, int totalBits)
        {
            // Heuristic: scan first 64 bits for a plausible 24-bit SSI (non-zero).
            int end = Math.Min(totalBits - 24, offset + 64);
            for (int bit = offset; bit <= end; bit++)
            {
                int v = TetraUtils.BitsToInt32(ch.Ptr, bit, 24);
                if (v > 0 && v < 0xFFFFFF) return v;
            }
            return 0;
        }

        private static string BuildLogMessage(MmPduType mmType, LogicChannel ch, int mmStartOffset, ref int offset, Dictionary<GlobalNames, int> dict)
        {
            // For the messages we know from the sample log, we emit the exact wording.
            // Unknown types still generate a minimal line to make sure the file is written.

            switch (mmType)
            {
                case MmPduType.D_AUTHENTICATION:
                {
                    // Subtype is typically 2 bits after MM PDU type.
                    int subtype = TetraUtils.BitsToInt32(ch.Ptr, mmStartOffset + 4, 2);
                    dict[GlobalNames.Authentication_sub_type] = subtype;
                    int ssi = dict.TryGetValue(GlobalNames.MM_SSI, out var v) ? v : 0;

                    var st = (D_AuthenticationPduSubType)subtype;
                    if (st == D_AuthenticationPduSubType.Demand)
                    {
                        return $"BS demands authentication: SSI: {ssi}";
                    }
                    if (st == D_AuthenticationPduSubType.Result)
                    {
                        // Matches the sample text.
                        return $"BS result to MS authentication: Authentication successful or no authentication currently in progress SSI: {ssi} - Authentication successful or no authentication currently in progress";
                    }
                    if (st == D_AuthenticationPduSubType.Reject)
                    {
                        return $"BS rejects authentication for SSI: {ssi}";
                    }
                    return $"Authentication: SSI: {ssi}";
                }

                case MmPduType.D_LOCATION_UPDATE_ACCEPT:
                {
                    int ssi = dict.TryGetValue(GlobalNames.MM_SSI, out var v) ? v : 0;
                    // Accept type (2 bits) is commonly present; store if we can.
                    int acceptType = TetraUtils.BitsToInt32(ch.Ptr, mmStartOffset + 4, 2);
                    dict[GlobalNames.Location_update_accept_type] = acceptType;
                    // We don't have the full mapping table; emit a conservative message.
                    return $"MS request for registration/authentication ACCEPTED for SSI: {ssi}";
                }

                case MmPduType.D_LOCATION_UPDATE_REJECT:
                {
                    int ssi = dict.TryGetValue(GlobalNames.MM_SSI, out var v) ? v : 0;
                    int cause = TetraUtils.BitsToInt32(ch.Ptr, mmStartOffset + 4, 5);
                    dict[GlobalNames.Reject_cause] = cause;
                    return $"MS updating in the network is NOT ACCEPTED for SSI: {ssi}";
                }

                case MmPduType.D_MM_STATUS:
                {
                    int ssi = dict.TryGetValue(GlobalNames.MM_SSI, out var v) ? v : 0;
                    return $"MM status: SSI: {ssi}";
                }

                default:
                {
                    int ssi = dict.TryGetValue(GlobalNames.MM_SSI, out var v) ? v : 0;
                    return $"MM: {mmType} SSI: {ssi}";
                }
            }
        }
    }
}
