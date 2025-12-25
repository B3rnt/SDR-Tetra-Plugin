using SDRSharp.Common;
using SDRSharp.Radio;
using System;
using System.Collections.Generic;

namespace SDRSharp.Tetra
{
    /// <summary>
    /// Registers SDR# stream hooks exactly once and broadcasts IQ/Audio buffers
    /// to any number of TetraPanel subscribers.
    ///
    /// Why: registering new hooks while SDR# is running can stall the DSP/UI
    /// pipeline (e.g., frozen waterfall). This hub avoids that by keeping only
    /// one IQ hook and one audio hook, regardless of how many plugin windows
    /// are opened.
    /// </summary>
    public sealed unsafe class SharedStreamHub
    {
        private static readonly object _sync = new object();
        private static readonly Dictionary<ISharpControl, SharedStreamHub> _instances = new();

        private readonly ISharpControl _control;
        private readonly IFProcessor _ifProcessor;
        private readonly AudioProcessor _audioProcessor;

        public event IFProcessor.IQReadyDelegate IQReady;
        public event AudioProcessor.AudioReadyDelegate AudioReady;

        private SharedStreamHub(ISharpControl control)
        {
            _control = control;

            _ifProcessor = new IFProcessor();
            // Wideband IQ (same trick as AuxVFO): ProcessorType 0
            _control.RegisterStreamHook(_ifProcessor, (ProcessorType)0);
            // Ensure data starts flowing immediately
            _ifProcessor.Enabled = true;
            _ifProcessor.IQReady += (buf, sr, len) => IQReady?.Invoke(buf, sr, len);

            _audioProcessor = new AudioProcessor();
            _control.RegisterStreamHook(_audioProcessor, ProcessorType.DemodulatorOutput);
            _audioProcessor.Enabled = true;
            _audioProcessor.AudioReady += (buf, sr, len) => AudioReady?.Invoke(buf, sr, len);
        }

        public static SharedStreamHub GetOrCreate(ISharpControl control)
        {
            lock (_sync)
            {
                if (!_instances.TryGetValue(control, out var hub))
                {
                    hub = new SharedStreamHub(control);
                    _instances[control] = hub;
                }
                return hub;
            }
        }
    }
}
