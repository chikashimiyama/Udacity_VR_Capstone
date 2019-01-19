using UnityEngine;

namespace DomainF
{
    public class Controller
    {
        private IControllerState currentState_;
        private readonly IStateAssigner stateAssigner_;
        private readonly IControllerBehaviour controllerBehaviour_;
        private float distance_ = 15f;

        public Controller(IStateAssigner stateAssigner, IControllerBehaviour controllerBehaviour)
        {
            stateAssigner_ = stateAssigner;
            controllerBehaviour_ = controllerBehaviour;
            
            controllerBehaviour_.TransformChanged += OnTransformChanged;
            controllerBehaviour_.TriggerPressed += OnTriggerPressed;
            controllerBehaviour_.TriggerReleased += OnTriggerReleased;
            controllerBehaviour_.ThumbstickPositionChanged += OnThumbStickPositionChanged;
            
            controllerBehaviour_.Updated += OnUpdated;
            
            currentState_ = stateAssigner_.Unassign(this);
            currentState_.OnStateSelected();
        }

        private void OnTransformChanged(Transform transform)
        {
            currentState_.OnTransformChanged(transform);

            var angle = transform.rotation.eulerAngles.z;
            var limited = MathUtility.LimitKnobAngle(angle);
            controllerBehaviour_.WaveformInterpolationBehaviour.Angle = limited;
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

        private void OnUpdated()
        {
            controllerBehaviour_.DrawLaser();
            currentState_.OnUpdated();
        }

        private void OnFreqChanged(float freq)
        {
            controllerBehaviour_.IndicatorBehaviour.FreqText = freq.ToString("F2");
        }
        
        private void OnAmpChanged(float amp)
        {
            controllerBehaviour_.IndicatorBehaviour.AmpText = amp.ToString("F2");
        }

        private void OnWaveformUpdated(float[] samples)
        {
            controllerBehaviour_.DrawWaveform(samples);
        }

        private void OnResonanceChanged(float freq)
        {
            controllerBehaviour_.IndicatorBehaviour.ResText = freq.ToString("F2");
        }
        
        private void ChangeStateTo(IControllerState nextState)
        {
            currentState_.OnStateDeselected();
            currentState_.FreqChanged -= OnFreqChanged;
            currentState_.AmpChanged -= OnAmpChanged;
            currentState_.WaveformUpdated -= OnWaveformUpdated;
            currentState_.ResonanceChanged -= OnResonanceChanged;

            
            currentState_ = nextState;
            controllerBehaviour_.IndicatorBehaviour.FuncText = currentState_.Identifier;
            controllerBehaviour_.WaveVisibility = currentState_.Identifier != "idle";   
            
            currentState_.OnStateSelected();
            currentState_.OnDistanceChanged(distance_);
            currentState_.FreqChanged += OnFreqChanged;
            currentState_.AmpChanged += OnAmpChanged;
            currentState_.WaveformUpdated += OnWaveformUpdated;
            currentState_.ResonanceChanged += OnResonanceChanged;
        }
    }
}