using System;
using DomainF;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_Controller
    {
        private IControllerState carrierControllerStateMock_;
        private IControllerState modulatorControllerStateMock_;

        private IStateAssigner stateAssignerMock_;
        private IControllerBehaviour controllerBehaviourMock_;
        private Controller controller_;

        [SetUp]
        public void SetUp()
        {
            carrierControllerStateMock_ = Substitute.For<IControllerState>();
            modulatorControllerStateMock_ = Substitute.For<IControllerState>();
            stateAssignerMock_ = Substitute.For<IStateAssigner>();
            controllerBehaviourMock_ = Substitute.For<IControllerBehaviour>();

            controller_ = new Controller(stateAssignerMock_, controllerBehaviourMock_);
        }

        [Test]
        public void Construction_OnTriggerPressed_first_time()
        {
            stateAssignerMock_.Assign(Arg.Any<object>()).Returns(carrierControllerStateMock_);
            controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();

            stateAssignerMock_.Received(1).Assign(Arg.Any<object>());
            carrierControllerStateMock_.Received(1).OnStateSelected();
        }

        [Test]
        public void Construction_OnTriggerPressed_second_time_same_state()
        {
            stateAssignerMock_.Assign(Arg.Any<object>()).Returns(carrierControllerStateMock_);

            controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();
            controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();

            stateAssignerMock_.Received(2).Assign(Arg.Any<object>());
            carrierControllerStateMock_.Received(2).OnStateSelected();
        }

        [Test]
        public void Construction_OnTriggerPressed_second_time_different_state()
        {
            stateAssignerMock_.Assign(Arg.Any<object>())
                .Returns(carrierControllerStateMock_, modulatorControllerStateMock_);

            controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();
            controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();

            stateAssignerMock_.Received(2).Assign(Arg.Any<object>());
            carrierControllerStateMock_.Received(1).OnStateSelected();
        }

        [Test]
        public void Construction_OnTriggerReleased()
        {
            controllerBehaviourMock_.TriggerReleased += Raise.Event<Action>();

            stateAssignerMock_.Received(2).Unassign(Arg.Any<object>());
        }
    }
}