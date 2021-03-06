using System;
using UnityEngine;

namespace DomainF
{
    public class ModulatorState : IControllerState
    {
        private readonly IPureDataFacade pureDataFacade_;
        private readonly IPureDataArrayFacade pureDataArrayFacade_;
        private const string IdentifierString = "modulator";

        public ModulatorState(IPureDataFacade pureDataFacade, IPureDataArrayFacade pureDataArrayFacade = null)
        {
            pureDataFacade_ = pureDataFacade;
            if(pureDataArrayFacade == null)
                pureDataArrayFacade_ = new PureDataArrayFacade(Identifier, 512);
        }

        public string Identifier
        {
            get { return IdentifierString; }
        }

        public void OnStateSelected()
        {
            pureDataFacade_.SendMessage("mod_active", 1f);
        }

        public void OnStateDeselected()
        {
            pureDataFacade_.SendMessage("mod_active", 0f);
        }

        public void OnDistanceChanged(float distance)
        {
            var amp = MathUtility.DistanceToAmp(distance) * 1000f;
            pureDataFacade_.SendMessage("mod_amp", amp);
            if(AmpChanged != null)
                AmpChanged.Invoke(amp);
        }

        public void OnTransformChanged(Transform transform)
        {
            var linear = MathUtility.EulerAngleToLinear(transform.eulerAngles.x);
            var freq = Mathf.Pow(linear, 2.6f)  * 1760f;
            var waveform = MathUtility.EulerToUnipolar(transform.rotation.eulerAngles.z);
            var resonance = MathUtility.EulerAngleToReson(transform.rotation.eulerAngles.y);

            pureDataFacade_.SendMessage("mod_freq", freq);
            pureDataFacade_.SendMessage("mod_waveform", waveform);
            pureDataFacade_.SendMessage("mod_reson", resonance);
            
            if(FreqChanged != null) FreqChanged.Invoke(freq);
            if(ResonanceChanged != null) ResonanceChanged.Invoke(resonance);
        }

        public void OnUpdated()
        {
            if (WaveformUpdated != null) 
                WaveformUpdated.Invoke(pureDataArrayFacade_.Get());
        }
        
        public event Action<float[]> WaveformUpdated;
        public event Action<float> FreqChanged;
        public event Action<float> AmpChanged;
        public event Action<float> ResonanceChanged;
    }
}