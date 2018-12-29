using UnityEngine;

namespace DomainF
{
    public class Controller
    {
        private IControllerState currentState_;
        private readonly IStateAssigner stateAssigner_;
        private readonly IControllerBehaviour controllerBehaviour_;
        private readonly IIndicatorBehaviour indicatorBehaviour_;
        private float distance_;

        public Controller(IStateAssigner stateAssigner, IControllerBehaviour controllerBehaviour,
            IIndicatorBehaviour indicatorBehaviour)
        {
            stateAssigner_ = stateAssigner;
            controllerBehaviour_ = controllerBehaviour;
            indicatorBehaviour_ = indicatorBehaviour;
            
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
            currentState_.FreqChanged -= OnFreqChanged;
            currentState_.AmpChanged -= OnAmpChanged;
            
            currentState_ = nextState;

            currentState_.OnStateSelected();
            currentState_.FreqChanged += OnFreqChanged;
            currentState_.AmpChanged += OnAmpChanged;
        }

        private void OnFreqChanged(float freq)
        {
            indicatorBehaviour_.FreqText = "Freq: " + freq.ToString("F2");
        }
        
        private void OnAmpChanged(float amp)
        {
            indicatorBehaviour_.AmpText = " Amp: " + amp.ToString("F2");
        }
    }
}