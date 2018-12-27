
namespace DomainF
{
    public class Controller 
    {
        private IControllerState currentState_;
        private IControllerBehaviour controllerBehaviour_;
        private readonly IStateAssigner stateAssigner_;
        
        public Controller(IStateAssigner stateAssigner, IControllerBehaviour controllerBehaviour)
        {
            stateAssigner_ = stateAssigner;
            controllerBehaviour_ = controllerBehaviour;
        }


        private void OnTriggerPressed()
        {
            
        }

        private void OnTriggerRelease()
        {
            
        }
                
        private void Activate()
        {
            var nextState = stateAssigner_.Assign(this);
            currentState_.OnStateDeselected();
            currentState_ = nextState;
            currentState_.OnStateSelected();
        }

        private void Deactivate()
        {
            stateAssigner_.Unassign(this);
        }

    }
}