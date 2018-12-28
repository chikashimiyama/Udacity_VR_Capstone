using System;
using UnityEngine;

namespace DomainF
{
    public class CarrierState : IControllerState
    {
        private readonly IPureDataFacade pureDataFacade_;
        
        public CarrierState(IPureDataFacade pureDataFacade)
        {
            pureDataFacade_ = pureDataFacade;
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
            pureDataFacade_.SendMessage("car_freq", freq);
            if(FreqChanged!= null)
                FreqChanged.Invoke(freq);
        }

        public event Action<float> FreqChanged; 
        public event Action<float> AmpChanged;
    }
}