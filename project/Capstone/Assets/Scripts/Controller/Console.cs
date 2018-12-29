namespace DomainF
{
    public class Console
    {
        private readonly IShowable scaleGridShowable_;
        private readonly IShowable directionGridShowable_;

        public Console(IConsoleBehaviour consoleBehaviour, IShowable scaleGridShowable, IShowable directionGridShowable,
            IComponentFactory componentFactory = null)
        {
            componentFactory = componentFactory ?? new CompomentFactory();

            directionGridShowable_ = directionGridShowable;
            scaleGridShowable_ = scaleGridShowable;

            var scaleGridToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.ScaleGridToggleBehaviour);
            scaleGridToggleButton.ButtonStateChanged += OnScaleGridButtonTouched;

            var directionGridToggleButton =
                componentFactory.CreateToggleButton(consoleBehaviour.DirectionGridToggleBehaviour);
            directionGridToggleButton.ButtonStateChanged += OnDirectionGridButtonTouched;
        }

        private void OnScaleGridButtonTouched(bool state)
        {
            scaleGridShowable_.Visible = state;
        }

        private void OnDirectionGridButtonTouched(bool state)
        {
            directionGridShowable_.Visible = state;
        }
    }
}