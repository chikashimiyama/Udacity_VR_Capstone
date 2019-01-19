using System;
using UnityEngine;

namespace DomainF
{
    public class IdleState : IControllerState
    {
        private const string IdentifierString = "idle";
        public string Identifier
        {
            get { return IdentifierString; }
        }



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

        public void OnUpdated()
        {
        }

        public event Action<float[]> WaveformUpdated;
        public event Action<float> FreqChanged;
        public event Action<float> AmpChanged;
    }
}