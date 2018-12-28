using UnityEngine;

namespace DomainF
{
    public class CarrierState : IControllerState
    {
        private IPureDataFacade pureDataFacade_;

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
            pureDataFacade_.SendMessage("car_amp", MathUtility.DistanceToAmp(distance));
        }

        public void OnTransformChanged(Transform transform)
        {
            var freq = MathUtility.EulerAngleToFrequency(transform.eulerAngles.x);
            pureDataFacade_.SendMessage("car_freq", freq);
        }
    }
}