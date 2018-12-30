namespace DomainF
{
    public interface IComponentFactory
    {
        IToggleButton CreateToggleButton(IToggleButtonBehaviour toggleButtonBehaviour);
    }
    
    public class ComponentFactory : IComponentFactory
    {
        public IToggleButton CreateToggleButton(IToggleButtonBehaviour toggleButtonBehaviour)
        {
            return new ToggleButton(toggleButtonBehaviour);
        }
    }
}