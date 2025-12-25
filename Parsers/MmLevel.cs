namespace SDRSharp.Tetra
{
    unsafe class MmLevel
    {
        // Based on the reference Class18 rules for MM PDUs.
        private readonly Rules[] _locationUpdateAcceptRules = new Rules[]
        {
            new Rules(GlobalNames.Location_update_accept_type, 3),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_SSI, 24),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            // In the reference code this is marked Reserved (24). We use it as MM_GSSI when present.
            new Rules(GlobalNames.MM_GSSI, 24),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            // In the reference code this is Reserved (16). We use low 4 bits as CCK_identifier.
            new Rules(GlobalNames.CCK_identifier, 16),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.Reserved, 14, RulesType.Reserved),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.Reserved, 6, RulesType.Reserved)
        };

        private readonly Rules[] _locationUpdateRejectRules = new Rules[]
        {
            new Rules(GlobalNames.Location_update_type, 3),
            new Rules(GlobalNames.Reject_cause, 5),
            new Rules(GlobalNames.Cipher_control, 1),
            new Rules(GlobalNames.Ciphering_parameters, 10, RulesType.Switch, (int)GlobalNames.Cipher_control, 1),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_Address_extension, 24)
        };

        private readonly Rules[] _authenticationRules = new Rules[]
        {
            new Rules(GlobalNames.Authentication_sub_type, 2),
            new Rules(GlobalNames.Options_bit, 1, RulesType.Options_bit),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_SSI, 24),
            new Rules(GlobalNames.Presence_bit, 1, RulesType.Presence_bit, 1),
            new Rules(GlobalNames.MM_Address_extension, 24)
        };

        public void Parse(LogicChannel channelData, int offset, ReceivedData result)
        {
            var pduType = (MmPduType)TetraUtils.BitsToInt32(channelData.Ptr, offset, 4);
            offset += 4;
            result.Add(GlobalNames.MM_PDU_Type, (int)pduType);

            switch (pduType)
            {
                case MmPduType.D_AUTHENTICATION:
                    Global.ParseParams(channelData, offset, _authenticationRules, result);
                    break;

                case MmPduType.D_LOCATION_UPDATE_ACCEPT:
                    Global.ParseParams(channelData, offset, _locationUpdateAcceptRules, result);
                    break;

                case MmPduType.D_LOCATION_UPDATE_REJECT:
                    Global.ParseParams(channelData, offset, _locationUpdateRejectRules, result);
                    break;
            }

            // Write MM registrations log.
            MmRegistrationsLogger.Log(result);
        }
    }
}
