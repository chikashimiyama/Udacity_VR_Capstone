using System;

namespace DomainF
{
    public interface ITutorial
    {
        void ShowNextInstruction();
        void Start();
        void Terminate();
    }

    public class Tutorial : ITutorial
    {
        private int index_;
        private readonly ITutorialBehaviour tutorialBehaviour_;

        public Tutorial(ITutorialBehaviour tutorialBehaviour)
        {
            tutorialBehaviour_ = tutorialBehaviour;
            Start();
        }
        
        public void Start()
        {
            index_ = 0;
            tutorialBehaviour_.Open();
            ShowNextInstruction();
        }

        public void ShowNextInstruction()
        {
            if(index_ < instructions_.Length)
                tutorialBehaviour_.SetText(instructions_[index_++]);
            else
            {
                tutorialBehaviour_.Close();
            }
        }
        
        public void Terminate()
        {
            tutorialBehaviour_.Close();
        }

        public readonly string[] instructions_=
        {
            "Welcome To Domain F. An audio visual synthesizer. Touch the button bellow to start tutorial.",
            "Extend one of your arms and shoot forward by pressing the trigger button to generate sound",
            "While pressing the trigger, you can move your hand. The vertical movement controls the loudness, the horizontal movement controls the pitch",
            "You can use the second controller to modify the sound property of the sound",
            "The distance and the angle between two controllers control the timbre and sound modulation"
        };
    }
}