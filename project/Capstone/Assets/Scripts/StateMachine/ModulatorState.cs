using UnityEngine;

namespace DomainF
{
    public class ModulatorState : IControllerState
    {
        private IPureDataFacade pureDataFacade_;

        public ModulatorState(IPureDataFacade pureDataFacade)
        {
            pureDataFacade_ = pureDataFacade;
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
            pureDataFacade_.SendMessage("mod_amp", MathUtility.DistanceToAmp(distance));
        }

        public void OnTransformChanged(Transform transform)
        {
            var freq = MathUtility.EulerAngleToLinear(transform.eulerAngles.x);
            pureDataFacade_.SendMessage("mod_freq", freq);
        }
    }
}