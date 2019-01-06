namespace DomainF
{
    public class Console
    {
        private readonly IConsoleBehaviour consoleBehaviour_;
        private readonly IGridBehaviour gridBehaviour_;
     
        public Console(IConsoleBehaviour consoleBehaviour, IGridBehaviour gridBehaviour,
            IComponentFactory componentFactory = null)
        {
            consoleBehaviour_ = consoleBehaviour;
            gridBehaviour_ = gridBehaviour;
            componentFactory = componentFactory ?? new ComponentFactory();

            var scaleGridToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.ScaleGridToggleBehaviour);
            scaleGridToggleButton.ButtonStateChanged += OnScaleGridButtonTouched;

            var directionGridToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.DirectionGridToggleBehaviour);
            directionGridToggleButton.ButtonStateChanged += OnDirectionGridButtonTouched;

            var equatorToggleButton = componentFactory.CreateToggleButton(consoleBehaviour.EquatorToggleBehaviour);
            equatorToggleButton.ButtonStateChanged += OnEquatorButtonTouched;
            
            consoleBehaviour.ConsoleEntered += OnConsoleEntered;
            consoleBehaviour.ConsoleExited += OnConsoleExited;
        }

        private void OnScaleGridButtonTouched(bool state)
        {
            gridBehaviour_.ScaleCircleBehaviour.State = state;
        }

        private void OnDirectionGridButtonTouched(bool state)
        {
            gridBehaviour_.DirectionCircleBehaviour.State = state;
        }

        private void OnEquatorButtonTouched(bool state)
        {
            gridBehaviour_.EquatorCircleBehaviour.State = state;
        }

        private void OnConsoleEntered()
        {
            consoleBehaviour_.ScaleGridToggleBehaviour.Annotate = true;
            consoleBehaviour_.DirectionGridToggleBehaviour.Annotate = true;
            consoleBehaviour_.EquatorToggleBehaviour.Annotate = true;
        }
        
        private void OnConsoleExited()
        {
            consoleBehaviour_.ScaleGridToggleBehaviour.Annotate = false;
            consoleBehaviour_.DirectionGridToggleBehaviour.Annotate = false;
            consoleBehaviour_.EquatorToggleBehaviour.Annotate = false;
        }
    }
}