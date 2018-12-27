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
            pureDataFacade_.SendMessage("note", 1.0f);
        }

        public void OnStateDeselected()
        {
            pureDataFacade_.SendMessage("note", 0.0f);
        }

        public void OnThumbStickUpdated()
        {
        }
    }
}