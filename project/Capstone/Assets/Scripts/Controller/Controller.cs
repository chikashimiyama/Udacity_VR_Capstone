using UnityEngine;

namespace DomainF
{
    public class Controller 
    {
        private IControllerState currentState_;
        private readonly IStateAssigner stateAssigner_;
        private readonly IControllerBehaviour controllerBehaviour_;
        private float distance_;
        
        public Controller(IStateAssigner stateAssigner, IControllerBehaviour controllerBehaviour)
        {
            stateAssigner_ = stateAssigner;
            controllerBehaviour_ = controllerBehaviour;
            controllerBehaviour_.TransformChanged += OnTransformChanged;
            controllerBehaviour_.TriggerPressed += OnTriggerPressed;
            controllerBehaviour_.TriggerReleased += OnTriggerReleased;
            controllerBehaviour_.ThumbstickPositionChanged += OnThumbStickPositionChanged;
            currentState_ = stateAssigner_.Unassign(this);
        }

        private void OnTransformChanged(Transform transform)
        {
            currentState_.OnTransformChanged(transform);
        }
        
        private void OnTriggerPressed()
        {
            ChangeStateTo(stateAssigner_.Assign(this));
        }

        private void OnTriggerReleased()
        {
            ChangeStateTo(stateAssigner_.Unassign(this));
        }

        private void OnThumbStickPositionChanged(Vector2 position)
        {
            var movement = position.y;
            distance_ += movement;
            distance_ = MathUtility.LimitDistance(distance_);
            controllerBehaviour_.LaserLength = distance_;
            currentState_.OnDistanceChanged(distance_);
        }

        private void ChangeStateTo(IControllerState nextState)
        {
            currentState_.OnStateDeselected();
            currentState_ = nextState;
            currentState_.OnStateSelected();
        }
    }
}