namespace DomainF
{
    public class ModulatorState : IControllerState
    {
        private IPureDataFacade pureDataFacade_;
        
        public ModulatorState(IPureDataFacade pureDataFacade)
        {
            pureDataFacade_ = pureDataFacade;
        }
        
        public void OnStateSelected()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateDeselected()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerPressed()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerReleased()
        {
            throw new System.NotImplementedException();
        }

        public void OnThumbStickUpdated()
        {
            throw new System.NotImplementedException();
        }
    }
}