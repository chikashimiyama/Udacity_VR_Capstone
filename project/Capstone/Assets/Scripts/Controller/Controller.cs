
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
            var nextState = stateAssigner_.Assign(this);

            if (currentState_ == nextState) return;
            
            if(currentState_ != null)
                currentState_.OnStateDeselected();
            currentState_ = nextState;
            currentState_.OnStateSelected();
        }

        private void OnTriggerReleased()
        {
            stateAssigner_.Unassign(this);
        }


    }
}