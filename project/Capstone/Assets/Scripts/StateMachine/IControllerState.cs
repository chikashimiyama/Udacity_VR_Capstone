using UnityEngine;

namespace DomainF
{
    public interface IControllerState
    {
        void OnStateSelected();
        void OnStateDeselected();
        void OnDistanceChanged(float distance);
        void OnTransformChanged(Transform transform);
    }
}