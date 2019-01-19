using System;
using DomainF;
using NUnit.Framework;
using NSubstitute;
using Console = DomainF.Console;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_Console
    {
        private Console console_;

        private IToggleButtonBehaviour scaleGridToggleBehaviourMock_;
        private IToggleButtonBehaviour directionGridToggleBehaviourMock_;
        private IToggleButtonBehaviour equatorToggleBehaviourMock_;

        private IToggleButton scaleGridToggleButtonMock_;
        private IToggleButton directionGridToggleButtonMock_;
        private IToggleButton equatorGridToggleButtonMock_;

        private IVisible scaleCircleBehaviourMock_;
        private IVisible directionCircleBehaviourMock_;
        private IVisible equatorCircleBehaviourMock_;

        private IConsoleBehaviour consoleBehaviourMock_;
        private IGridBehaviour gridBehaviourMock_;
        private IComponentFactory componentFactoryMock_;

        private IPureDataFacade pureDataFacadeMock_;
        
        [SetUp]
        public void SetUp()
        {
            scaleGridToggleBehaviourMock_ = Substitute.For<IToggleButtonBehaviour>();
            directionGridToggleBehaviourMock_ = Substitute.For<IToggleButtonBehaviour>();
            equatorToggleBehaviourMock_ = Substitute.For<IToggleButtonBehaviour>();
            scaleGridToggleButtonMock_ = Substitute.For<IToggleButton>();
            directionGridToggleButtonMock_ = Substitute.For<IToggleButton>();
            equatorGridToggleButtonMock_ = Substitute.For<IToggleButton>();
            consoleBehaviourMock_ = Substitute.For<IConsoleBehaviour>();
            gridBehaviourMock_ = Substitute.For<IGridBehaviour>();
            componentFactoryMock_ = Substitute.For<IComponentFactory>();
            scaleCircleBehaviourMock_ = Substitute.For<IVisible>();
            directionCircleBehaviourMock_ = Substitute.For<IVisible>();
            equatorCircleBehaviourMock_ = Substitute.For<IVisible>();
            pureDataFacadeMock_ = Substitute.For<IPureDataFacade>();
            
            consoleBehaviourMock_.ScaleGridToggleBehaviour.Returns(scaleGridToggleBehaviourMock_);
            consoleBehaviourMock_.DirectionGridToggleBehaviour.Returns(directionGridToggleBehaviourMock_);
            consoleBehaviourMock_.EquatorToggleBehaviour.Returns(equatorToggleBehaviourMock_);

            componentFactoryMock_.CreateToggleButton(scaleGridToggleBehaviourMock_).Returns(scaleGridToggleButtonMock_);
            componentFactoryMock_.CreateToggleButton(directionGridToggleBehaviourMock_).Returns(directionGridToggleButtonMock_);
            componentFactoryMock_.CreateToggleButton(equatorToggleBehaviourMock_).Returns(equatorGridToggleButtonMock_);

            gridBehaviourMock_.ScaleCircleBehaviour.Returns(scaleCircleBehaviourMock_);
            gridBehaviourMock_.DirectionCircleBehaviour.Returns(directionCircleBehaviourMock_);
            gridBehaviourMock_.FFTCircleBehaviour.Returns(equatorCircleBehaviourMock_);
            
            console_ = new Console(consoleBehaviourMock_, gridBehaviourMock_, pureDataFacadeMock_, componentFactoryMock_);
        }
    
        [Test]
        public void Construction()
        {
            consoleBehaviourMock_.ScaleGridToggleBehaviour.Received(1);
            consoleBehaviourMock_.DirectionGridToggleBehaviour.Received(1);
            consoleBehaviourMock_.EquatorToggleBehaviour.Received(1);
            
            componentFactoryMock_.Received(1).CreateToggleButton(scaleGridToggleBehaviourMock_);
            componentFactoryMock_.Received(1).CreateToggleButton(directionGridToggleBehaviourMock_);
            componentFactoryMock_.Received(1).CreateToggleButton(equatorToggleBehaviourMock_);
        }

        [Test]
        public void Construction_OnScaleGridButtonTouched()
        {
            scaleGridToggleButtonMock_.ButtonStateChanged += Raise.Event<Action<bool>>(true);
            scaleGridToggleButtonMock_.ButtonStateChanged += Raise.Event<Action<bool>>(false);

            scaleCircleBehaviourMock_.Received(1).State = true;
            scaleCircleBehaviourMock_.Received(1).State = false;
        }

        [Test]
        public void Construction_OnDirectionGridButtonTouched()
        {
            directionGridToggleButtonMock_.ButtonStateChanged += Raise.Event<Action<bool>>(true);
            directionGridToggleButtonMock_.ButtonStateChanged += Raise.Event<Action<bool>>(false);

            directionCircleBehaviourMock_.Received(1).State = true;
            directionCircleBehaviourMock_.Received(1).State = false;

        }

        [Test]
        public void Construction_OnEquatorButtonTouched()
        {
            equatorGridToggleButtonMock_.ButtonStateChanged += Raise.Event<Action<bool>>(true);
            equatorGridToggleButtonMock_.ButtonStateChanged += Raise.Event<Action<bool>>(false);

            equatorCircleBehaviourMock_.Received(1).State = true;
            equatorCircleBehaviourMock_.Received(1).State = false;
        }
    }
}