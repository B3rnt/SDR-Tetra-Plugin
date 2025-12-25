using System.Collections.Generic;

namespace SDRSharp.Tetra
{
    unsafe class MmLevel
    {
        private readonly Class18 _class18 = new Class18();

        public void Parse(LogicChannel channelData, int offset, ReceivedData result)
        {
            // Class18 works with a dictionary-like view.
            var dict = new Dictionary<GlobalNames, int>();

            // Copy already known globals that might be needed by the logger (eg Location_Area)
            if (result.Contains(GlobalNames.Location_Area))
                dict[GlobalNames.Location_Area] = result.Value(GlobalNames.Location_Area);

            _class18.method_0(channelData, ref offset, dict);

            // Merge parsed values back into ReceivedData.
            foreach (var kv in dict)
            {
                if (kv.Key == GlobalNames.Reserved) continue;
                result.SetValue(kv.Key, kv.Value);
            }
        }
    }
}
