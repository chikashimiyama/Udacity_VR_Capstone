namespace DomainF
{
    public interface IStateAssigner
    {
        IControllerState Assign(object controller);
        void Unassign(object controller);
    }
    
    public class StateAssigner : IStateAssigner
    {
        private readonly IControllerState carrierState_;
        private readonly IControllerState modulatorState_;

        private object carrierController_;

        public StateAssigner(IPureDataFacade pureDataFacade, IControllerStateFactory controllerStateFactory = null)
        {
            var stateFactory = controllerStateFactory ?? new ControllerStateFactory();
            carrierState_ = stateFactory.CreateCarrierState(pureDataFacade);
            modulatorState_ = stateFactory.CreateModulatorState(pureDataFacade);
        }

        public IControllerState Assign(object controller)
        {
            if (carrierController_ != null) 
                return modulatorState_;
            
            carrierController_ = controller;
            return carrierState_;

        }

        public void Unassign(object controller)
        {
            if (controller == carrierController_)
                carrierController_ = null;
        }
    }
}