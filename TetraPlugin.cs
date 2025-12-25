using SDRSharp.Common;
using System.Windows.Forms;
using System.Threading;

namespace SDRSharp.Tetra
{
    public class TetraPlugin : ISharpPlugin
    {
        private const string _displayName = "TETRA Demodulator";
        private ISharpControl _controlInterface;
        private TetraPanel _qpskPanel;
        private static int _pluginInstanceCounter;
        private int _instanceNumber;

        public UserControl Gui
        {
            get { return _qpskPanel; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public void Initialize(ISharpControl control)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _controlInterface = control;
            _instanceNumber = Interlocked.Increment(ref _pluginInstanceCounter);
            _qpskPanel = new TetraPanel(_controlInterface, _instanceNumber);
        }

        public void Close()
        {
            _qpskPanel.SaveSettings();
        }

    }
}
