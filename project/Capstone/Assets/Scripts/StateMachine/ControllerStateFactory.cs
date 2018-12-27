namespace DomainF
{
    public interface IControllerStateFactory
    {
        IControllerState CreateCarrierState();
        IControllerState CreateModulatorState();
    }

    public class ControllerStateFactory : IControllerStateFactory
    {
        public IControllerState CreateCarrierState()
        {
            return new CarrierState();
        }

        public IControllerState CreateModulatorState()
        {
            return new ModulatorState();
        }
    }
}