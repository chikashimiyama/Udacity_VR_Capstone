using System;

namespace DomainF
{
    public interface IToggleButton
    {
        void Toggle();
        bool State { get; }

        event Action<bool> ButtonStateChanged;
    }
    
    public class ToggleButton : IToggleButton
    {
        private IToggleButtonBehaviour toggleButtonBehaviour_;

        public ToggleButton(IToggleButtonBehaviour toggleButtonBehaviour)
        {
            toggleButtonBehaviour_ = toggleButtonBehaviour;
            toggleButtonBehaviour_.ButtonTouched += Toggle;
        }

        public void Toggle()
        {
            State = !State;
            toggleButtonBehaviour_.State = State;
            if(ButtonStateChanged != null)
                ButtonStateChanged.Invoke(State);
        }

        public bool State { get; private set; }
        public event Action<bool> ButtonStateChanged;
    }
}