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
            var linear = MathUtility.EulerAngleToLinear(transform.eulerAngles.x);
            var freq = MathUtility.MidiToFrequency(57f + linear * 24f); // 2 octaves
            
            var angle = 360 - transform.rotation.eulerAngles.z;
            if (angle > 180)
                angle -= 360;

            var param = Mathf.Clamp(angle, -120, 120) / -120f;
            pureDataFacade_.SendMessage("car_freq", freq);
            pureDataFacade_.SendMessage("car_waveform", param);

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