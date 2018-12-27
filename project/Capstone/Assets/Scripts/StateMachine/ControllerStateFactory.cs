namespace DomainF
{
    public interface IControllerStateFactory
    {
        IControllerState CreateCarrierState(IPureDataFacade pureDataFacade);
        IControllerState CreateModulatorState(IPureDataFacade pureDataFacade);
    }

    public class ControllerStateFactory : IControllerStateFactory
    {
        public IControllerState CreateCarrierState(IPureDataFacade pureDataFacade)
        {
            return new CarrierState(pureDataFacade);
        }

        public IControllerState CreateModulatorState(IPureDataFacade pureDataFacade)
        {    
            return new ModulatorState(pureDataFacade);
        }
    }
}