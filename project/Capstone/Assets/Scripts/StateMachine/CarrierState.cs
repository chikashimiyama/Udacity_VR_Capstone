namespace DomainF
{
    public class CarrierState : IControllerState
    {
        private IPureDataFacade pureDataFacade_;

        public CarrierState(IPureDataFacade pureDataFacade)
        {
            pureDataFacade_ = pureDataFacade;
        }
        
        public void OnStateSelected()
        {
        }

        public void OnStateDeselected()
        {
        }

        public void OnTriggerPressed()
        {
            pureDataFacade_.SendMessage("note", 1.0f);
        }

        public void OnTriggerReleased()
        {
            pureDataFacade_.SendMessage("note", 0.0f);
        }

        public void OnThumbStickUpdated()
        {
        }
    }
}