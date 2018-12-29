namespace DomainF
{
    public interface IStateAssigner
    {
        IControllerState Assign(object controller);
        IControllerState Unassign(object controller);
    }
    
    public class StateAssigner : IStateAssigner
    {
        private readonly IControllerState idleState_;
        private readonly IControllerState carrierState_;
        private readonly IControllerState modulatorState_;

        private object carrierController_;

        public StateAssigner(IPureDataFacade pureDataFacade,  IControllerStateFactory controllerStateFactory = null)
        {
            var stateFactory = controllerStateFactory ?? new ControllerStateFactory();
            idleState_ = stateFactory.CreateIdleState();
            carrierState_ = stateFactory.CreateCarrierState(pureDataFacade);
            modulatorState_ = stateFactory.CreateModulatorState(pureDataFacade);
        }

        public IControllerState Assign(object controller)
        {
            if (carrierController_ != null)
            {
                return modulatorState_;
            }

            carrierController_ = controller;
            return carrierState_;
        }

        public IControllerState Unassign(object controller)
        {
            if (controller == carrierController_)
                carrierController_ = null;

            return idleState_;
        }
    }
}