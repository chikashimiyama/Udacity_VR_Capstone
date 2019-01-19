using System;
using UnityEngine;

namespace DomainF
{
    public class CarrierState : IControllerState
    {
        private readonly IPureDataFacade pureDataFacade_;
        private readonly IPureDataArrayFacade pureDataArrayFacade_;
        private const string IdentifierString = "carrier";

        public CarrierState(IPureDataFacade pureDataFacade, IPureDataArrayFacade pureDataArrayFacade = null)
        {
            pureDataFacade_ = pureDataFacade;
            if (pureDataArrayFacade == null)
                pureDataArrayFacade_ = new PureDataArrayFacade(IdentifierString, 512);
        }

        public string Identifier
        {
            get { return IdentifierString; }
        }

        public void OnStateSelected()
        {
            pureDataFacade_.SendMessage("car_active", 1f);
        }

        public void OnStateDeselected()
        {
            pureDataFacade_.SendMessage("car_active", 0f);
        }

        public void OnDistanceChanged(float distance)
        {
            var amp = MathUtility.DistanceToAmp(distance);
            pureDataFacade_.SendMessage("car_amp", amp);
            if (AmpChanged != null)
                AmpChanged.Invoke(amp);
        }

        public void OnTransformChanged(Transform transform)
        {
            var pitch = MathUtility.EulerAngleToLinear(transform.eulerAngles.x);
            var freq = MathUtility.MidiToFrequency(57f + pitch * 24f); // 2 octaves
            var waveform = MathUtility.EulerToUnipolar(transform.rotation.eulerAngles.z);
            var resonance = MathUtility.EulerAngleToReson(transform.rotation.eulerAngles.y);
            
            pureDataFacade_.SendMessage("car_freq", freq);
            pureDataFacade_.SendMessage("car_waveform", waveform);
            pureDataFacade_.SendMessage("car_reson", resonance);
            
            if (FreqChanged != null)
                FreqChanged.Invoke(freq);
        }

        public void OnUpdated()
        {
            if(WaveformUpdated != null)
                WaveformUpdated.Invoke(pureDataArrayFacade_.Get());
        }

        public event Action<float[]> WaveformUpdated;
        public event Action<float> FreqChanged;
        public event Action<float> AmpChanged;
    }
}