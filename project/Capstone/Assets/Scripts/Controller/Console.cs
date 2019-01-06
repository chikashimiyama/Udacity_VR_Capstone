namespace DomainF
{
    public class Console
    {
        private readonly IConsoleBehaviour consoleBehaviour_;
     
        public Console(IConsoleBehaviour consoleBehaviour,
            IComponentFactory componentFactory = null)
        {
            consoleBehaviour_ = consoleBehaviour;
            componentFactory = componentFactory ?? new ComponentFactory();

            consoleBehaviour.ConsoleEntered += OnConsoleEntered;
            consoleBehaviour.ConsoleExited += OnConsoleExited;
            
            var scaleGridToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.ScaleGridToggleBehaviour);
            scaleGridToggleButton.ButtonStateChanged += OnScaleGridButtonTouched;

            var directionGridToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.DirectionGridToggleBehaviour);
            directionGridToggleButton.ButtonStateChanged += OnDirectionGridButtonTouched;

            var equatorToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.EquatorToggleBehaviour);
            equatorToggleButton.ButtonStateChanged += OnEquatorButtonTouched;
        }

        private void OnScaleGridButtonTouched(bool state)
        {
            consoleBehaviour_.ScaleGridToggleBehaviour.State = state;
        }

        private void OnDirectionGridButtonTouched(bool state)
        {
            consoleBehaviour_.DirectionGridToggleBehaviour.State = state;
        }

        private void OnEquatorButtonTouched(bool state)
        {
            consoleBehaviour_.EquatorToggleBehaviour.State = state;
        }

        private void OnConsoleEntered()
        {
            consoleBehaviour_.ScaleGridToggleBehaviour.Anotate = true;
            consoleBehaviour_.DirectionGridToggleBehaviour.Anotate = true;
            consoleBehaviour_.EquatorToggleBehaviour.Anotate = true;
        }
        
        private void OnConsoleExited()
        {
            consoleBehaviour_.ScaleGridToggleBehaviour.Anotate = false;
            consoleBehaviour_.DirectionGridToggleBehaviour.Anotate = false;
            consoleBehaviour_.EquatorToggleBehaviour.Anotate = false;
        }
    }
}