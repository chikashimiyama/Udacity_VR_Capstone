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
        }

        public void OnStateDeselected()
        {
            
        }

        public void OnThumbStickUpdated()
        {
            throw new System.NotImplementedException();
        }
    }
}