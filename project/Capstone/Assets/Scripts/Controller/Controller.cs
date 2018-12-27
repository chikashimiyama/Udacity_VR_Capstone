
namespace DomainF
{
    public class Controller 
    {
        private IControllerState currentState_;
        private readonly IStateAssigner stateAssigner_;
        
        public Controller(IStateAssigner stateAssigner, IControllerBehaviour controllerBehaviour)
        {
            stateAssigner_ = stateAssigner;
            controllerBehaviour.TriggerPressed += OnTriggerPressed;
            controllerBehaviour.TriggerReleased += OnTriggerReleased;
        }

        private void OnTriggerPressed()
        {
            ChangeStateTo(stateAssigner_.Assign(this));
        }

        private void OnTriggerReleased()
        {
            ChangeStateTo(stateAssigner_.Unassign(this));
            
        }

        private void ChangeStateTo(IControllerState nextState)
        {
            if (currentState_ == nextState) return;
            currentState_.OnStateDeselected();
            currentState_ = nextState;
            currentState_.OnStateSelected();
        }


    }
}