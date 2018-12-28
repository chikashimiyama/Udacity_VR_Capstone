using System;
using UnityEngine;

namespace DomainF
{
    public class IdleState : IControllerState
    {
        public void OnStateSelected()
        {
        }

        public void OnStateDeselected()
        {
        }

        public void OnDistanceChanged(float distance)
        {
        }

        public void OnTransformChanged(Transform transform)
        {
        }

        public event Action<float> FreqChanged;
        public event Action<float> AmpChanged;
    }
}