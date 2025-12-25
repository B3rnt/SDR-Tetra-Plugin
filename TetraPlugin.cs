using SDRSharp.Common;
using System.Threading;
using System.Windows.Forms;

namespace SDRSharp.Tetra
{
    public class TetraPlugin : ISharpPlugin
    {
        private const string _displayName = "TETRA Demodulator";
        private ISharpControl _controlInterface;
        private TetraMultiPanel _multiPanel;

        // Used to generate unique instance numbers for per-channel settings files.
        private static int _instanceCounter;

        internal static int NextInstanceNumber()
        {
            return Interlocked.Increment(ref _instanceCounter);
        }

        internal static void EnsureInstanceCounterAtLeast(int value)
        {
            int current;
            do
            {
                current = _instanceCounter;
                if (current >= value) return;
            }
            while (Interlocked.CompareExchange(ref _instanceCounter, value, current) != current);
        }

        public UserControl Gui => _multiPanel;

        public string DisplayName => _displayName;

        public void Initialize(ISharpControl control)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _controlInterface = control;
            _multiPanel = new TetraMultiPanel(_controlInterface);
        }

        public void Close()
        {
            _multiPanel?.SaveAll();
        }
    }
}
