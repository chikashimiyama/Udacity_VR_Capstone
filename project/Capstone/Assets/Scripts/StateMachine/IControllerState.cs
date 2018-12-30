using System;
using UnityEngine;

namespace DomainF
{
    public interface IControllerState
    {
        string Identifier { get; }
        void OnStateSelected();
        void OnStateDeselected();
        void OnDistanceChanged(float distance);
        void OnTransformChanged(Transform transform);
        void OnUpdated();

        event Action<float> FreqChanged;
        event Action<float> AmpChanged;
        event Action<float[]> WaveformUpdated;
    }
}