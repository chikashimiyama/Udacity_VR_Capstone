using DomainFo;
using UnityEngine;

namespace DomainF
{
    public class Controller
    {
        private IControllerState currentState_;
        private readonly IStateAssigner stateAssigner_;
        private readonly IControllerBehaviour controllerBehaviour_;
        private readonly IIndicatorBehaviour indicatorBehaviour_;
        private readonly IWaveformInterpolationBehaviour waveformInterpolationBehaviour_;
        private float distance_;

        public Controller(IStateAssigner stateAssigner, IControllerBehaviour controllerBehaviour,
            IIndicatorBehaviour indicatorBehaviour, IWaveformInterpolationBehaviour waveformInterpolationBehaviour)
        {
            stateAssigner_ = stateAssigner;
            controllerBehaviour_ = controllerBehaviour;
            indicatorBehaviour_ = indicatorBehaviour;
            waveformInterpolationBehaviour_ = waveformInterpolationBehaviour;
            
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

            waveformInterpolationBehaviour_.Angle = transform.rotation.eulerAngles.z;
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
            currentState_.OnUpdated();
        }

        private void ChangeStateTo(IControllerState nextState)
        {
            currentState_.OnStateDeselected();
            currentState_.FreqChanged -= OnFreqChanged;
            currentState_.AmpChanged -= OnAmpChanged;
            currentState_.WaveformUpdated -= OnWaveformUpdated;
            
            currentState_ = nextState;
            indicatorBehaviour_.FuncText = currentState_.Identifier;

            controllerBehaviour_.LaserVisibility = currentState_.Identifier != "Idle";   
            
            
            currentState_.OnStateSelected();
            currentState_.FreqChanged += OnFreqChanged;
            currentState_.AmpChanged += OnAmpChanged;
            currentState_.WaveformUpdated += OnWaveformUpdated;
        }

        private void OnFreqChanged(float freq)
        {
            indicatorBehaviour_.FreqText = "Freq: " + freq.ToString("F2");
        }
        
        private void OnAmpChanged(float amp)
        {
            indicatorBehaviour_.AmpText = " Amp: " + amp.ToString("F2");
        }

        private void OnWaveformUpdated(float[] samples)
        {
            controllerBehaviour_.DrawLaser();
            controllerBehaviour_.DrawWaveform(samples);
        }
    }
}