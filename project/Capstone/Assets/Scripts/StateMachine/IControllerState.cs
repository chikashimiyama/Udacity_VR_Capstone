using System;
using UnityEngine;

namespace DomainF
{
    public interface IControllerState
    {
        void OnStateSelected();
        void OnStateDeselected();
        void OnDistanceChanged(float distance);
        void OnTransformChanged(Transform transform);

        event Action<float> FreqChanged;
        event Action<float> AmpChanged;
    }
}