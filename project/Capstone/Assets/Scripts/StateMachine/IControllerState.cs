using UnityEngine;

namespace DomainF
{
    public interface IControllerState
    {
        void OnStateSelected();
        void OnStateDeselected();
        void OnDistanceChanged(float distance);
        void OnPoseUpdated(Transform transform);
    }
}