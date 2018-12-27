namespace DomainF
{
    public interface IControllerState
    {
        void OnStateSelected();
        void OnStateDeselected();
        void OnTriggerPressed();
        void OnTriggerReleased();
        void OnThumbStickUpdated();
    }
}