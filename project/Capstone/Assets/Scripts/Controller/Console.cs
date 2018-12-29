using Magicolo.GeneralTools;

namespace DomainF
{
    public class Console
    {
        private IToggleButton scaleGridToggleButton;
        private IShowable scaleGridShowable_;
        
        public Console(IConsoleBehaviour consoleBehaviour, IShowable scaleGridShowable, IComponentFactory componentFactory = null)
        {
            componentFactory = componentFactory ?? new CompomentFactory();
            
            scaleGridToggleButton =  componentFactory.CreateToggleButton(consoleBehaviour.ScaleGridToggleBehaviour);
            scaleGridToggleButton.ButtonStateChanged += OnScaleGridButtonTouched;
            scaleGridShowable_ = scaleGridShowable;
        }

        private void OnScaleGridButtonTouched(bool state)
        {
            scaleGridShowable_.Visible = state;
        }
    }
}