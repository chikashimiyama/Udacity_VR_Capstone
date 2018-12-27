namespace DomainF
{
    public class ModulatorState : IControllerState
    {
        private IPureDataFacade pureDataFacade_;
        private readonly float[] onValue_ = {1.0f};
        private readonly float[] offValue_ = {0.0f};

        public ModulatorState(IPureDataFacade pureDataFacade)
        {
            pureDataFacade_ = pureDataFacade;
        }
        
        public void OnStateSelected()
        {
            pureDataFacade_.SendMessage("mod_active", onValue_);
        }

        public void OnStateDeselected()
        {
            pureDataFacade_.SendMessage("mod_active", offValue_);
        }

        public void OnThumbStickUpdated()
        {
        }
    }
}