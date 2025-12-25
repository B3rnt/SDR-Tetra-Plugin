namespace SDRSharp.Tetra
{
    // MM PDU type mapping (4 bits) – matches the reference SDRtetra project.
    internal enum MmPduType
    {
        D_OTAR = 0,
        D_AUTHENTICATION = 1,
        D_CK_CHANGE_DEMAND = 2,
        D_DISABLE = 3,
        D_ENABLE = 4,
        D_LOCATION_UPDATE_ACCEPT = 5,
        D_LOCATION_UPDATE_COMMAND = 6,
        D_LOCATION_UPDATE_REJECT = 7,
        Reserved8 = 8,
        D_LOCATION_UPDATE_PROCEEDING = 9,
        D_ATTACH_DETACH_GROUP_IDENTITY = 10,
        D_ATTACH_DETACH_GROUP_IDENTITY_ACKNOWLEDGEMENT = 11,
        D_MM_STATUS = 12,
        Reserved13 = 13,
        Reserved14 = 14,
        MM_PDU_FUNCTION_NOT_SUPPORTED = 15
    }
}
