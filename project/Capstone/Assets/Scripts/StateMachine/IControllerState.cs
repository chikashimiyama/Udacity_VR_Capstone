namespace DomainF
{
    public interface IControllerState
    {
        void OnStateSelected();
        void OnStateDeselected();
        void OnThumbStickUpdated();
    }
}