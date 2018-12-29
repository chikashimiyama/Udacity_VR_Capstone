using System;
using UnityEngine;

namespace DomainF
{
    public class ModulatorState : IControllerState
    {
        private IPureDataFacade pureDataFacade_;
        private const string IdentifierString = "Modulator";

        public ModulatorState(IPureDataFacade pureDataFacade)
        {
            pureDataFacade_ = pureDataFacade;
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
            var freq = MathUtility.EulerAngleToLinear(transform.eulerAngles.x) * 1000f;
            pureDataFacade_.SendMessage("mod_freq", freq);
            if(FreqChanged != null)
                FreqChanged.Invoke(freq);
        }

        public event Action<float> FreqChanged;
        public event Action<float> AmpChanged;
    }
}