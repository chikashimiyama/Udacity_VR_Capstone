using NUnit.Framework;
using DomainF;
using NSubstitute;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_Tutorial
    {
        private ITutorialBehaviour tutorialBehaviourMock_;
        private Tutorial tutorial_;
        private const int numberOfTexts_ = 5;
        
        [SetUp]
        public void SetUp()
        {
            tutorialBehaviourMock_ = Substitute.For<ITutorialBehaviour>();
            tutorial_ = new Tutorial(tutorialBehaviourMock_);
        }

        [Test]
        public void Construction()
        {
            tutorialBehaviourMock_.Received(1).Open();
            tutorialBehaviourMock_.Received(1).SetText(tutorial_.instructions_[0]);
        }
        
        [Test]
        public void Start()
        {
            tutorial_.Start();
            
            tutorialBehaviourMock_.Received(2).Open();
            tutorialBehaviourMock_.Received(2).SetText(tutorial_.instructions_[0]);
        }
        
        [Test]
        public void Start_after_close()
        {
            for(var i = 0; i < numberOfTexts_; i++)
                tutorial_.ShowNextInstruction();
            
            tutorial_.Start();
            
            tutorialBehaviourMock_.Received(1).Close();
            tutorialBehaviourMock_.Received(2).SetText(tutorial_.instructions_[0]);
            tutorialBehaviourMock_.Received(1).SetText(tutorial_.instructions_[1]);
        }
        
        [Test]
        public void Start_after_terminate()
        {
            for(var i = 0; i < 3; i++)
                tutorial_.ShowNextInstruction();
            tutorial_.Terminate();
            tutorial_.Start();
            
            tutorialBehaviourMock_.Received(1).Close();
            tutorialBehaviourMock_.Received(2).SetText(tutorial_.instructions_[0]);
            tutorialBehaviourMock_.Received(1).SetText(tutorial_.instructions_[1]);
        }
        
        [Test]
        public void Start_during_iteration()
        {
            for(var i = 0; i < 3; i++)
                tutorial_.ShowNextInstruction();
            tutorial_.Start();
            
            tutorialBehaviourMock_.Received(2).SetText(tutorial_.instructions_[0]);
            tutorialBehaviourMock_.Received(1).SetText(tutorial_.instructions_[1]);
        }
        
        [Test]
        public void ShowNextInstruction()
        {
            for(var i = 0; i < numberOfTexts_; i++)
                tutorial_.ShowNextInstruction();
 
            for(var i = 0; i < numberOfTexts_; i++)
                tutorialBehaviourMock_.Received(1).SetText(tutorial_.instructions_[i]);

            tutorialBehaviourMock_.Received(1).Close();
        }
        
        [Test]
        public void ShowNextInstruction_exceeding_call_gracefully_fail()
        {
            for (var i = 0; i < 25; i++)
            {
                tutorial_.ShowNextInstruction();
            }
            
            tutorialBehaviourMock_.Received().Close();
        }

        [Test]
        public void Terminate()
        {
            tutorial_.Terminate();
            
            tutorialBehaviourMock_.Received(1).Close();
        }
    }
} 