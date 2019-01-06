using UnityEngine;

namespace DomainF
{
    public class Console
    {
        private readonly IConsoleBehaviour consoleBehaviour_;
        private readonly IGridBehaviour gridBehaviour_;
     
        public Console(IConsoleBehaviour consoleBehaviour, IGridBehaviour gridBehaviour, IPureDataFacade pureDataFacade,
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

            pureDataFacade.LevelChanged += OnLevelChanged;
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

        private void OnLevelChanged(float level)
        {
            var equator = (IColor)gridBehaviour_.EquatorCircleBehaviour;

            if(level < 1f)
                equator.color = Color.gray;
            else
            {
                var red = level / 200f + 0.5f;
                const float blue = 0.7f;
                var green = (100 - level) / 200f + 0.5f;
                equator.color = new Color(red, green, blue, 1f);
            }
        }
    }
}