using System;

namespace DomainF
{
    public interface ITutorial
    {
        void ShowNextInstruction();
    }

    public class Tutorial : ITutorial
    {
        private int index_ = 0;
        private ITutorialBehaviour tutorialBehaviour_;
        private string[] instructions_=
        {
            "Welcome To Domain F. An interactive FM-Synthesis simulator. Shoot Next Button to start tutorial.",
            "Extend your left arm and shoot forward by pressing the trigger button to generate sound",
            "While pressing the trigger, you can move your hand. The vertical movement controls the loudness, the horizontal movement controls the pitch",
            "You can use the second controller to modify the sound property of the sound"
        };
        
        public Tutorial(ITutorialBehaviour tutorialBehaviour)
        {
            tutorialBehaviour_ = tutorialBehaviour;
        }

        
        public void ShowNextInstruction()
        {
            tutorialBehaviour_.SetText(instructions_[++index_]);
        }
        
        

        public event Action NextButtonClicked;
        public event Action SkipButtonClicked;



        

    }
}