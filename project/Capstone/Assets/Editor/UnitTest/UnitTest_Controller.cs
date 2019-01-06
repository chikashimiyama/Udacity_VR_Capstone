using System;
using DomainF;
using DomainFo;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_Controller
    {
        private IControllerState carrierControllerStateMock_;
        private IControllerState modulatorControllerStateMock_;
        private IControllerState idleControllerStateMock_;

        private IStateAssigner stateAssignerMock_;
        private IControllerBehaviour controllerBehaviourMock_;
        private IIndicatorBehaviour indicatorBehaviourMock_;
        private IWaveformInterpolationBehaviour waveformInterpolationBehaviourMock_;
        

        [SetUp]
        public void SetUp()
        {
            carrierControllerStateMock_ = Substitute.For<IControllerState>();
            modulatorControllerStateMock_ = Substitute.For<IControllerState>();
            idleControllerStateMock_ = Substitute.For<IControllerState>();
            
            stateAssignerMock_ = Substitute.For<IStateAssigner>();
            controllerBehaviourMock_ = Substitute.For<IControllerBehaviour>();
            indicatorBehaviourMock_ = Substitute.For<IIndicatorBehaviour>();
            waveformInterpolationBehaviourMock_ = Substitute.For<IWaveformInterpolationBehaviour>();
        }

        [Test]
        public void Construction()
        {
            stateAssignerMock_.Unassign(Arg.Any<object>()).Returns(idleControllerStateMock_);

            var unused = new Controller(stateAssignerMock_, controllerBehaviourMock_, indicatorBehaviourMock_, waveformInterpolationBehaviourMock_);

            stateAssignerMock_.Received(1).Unassign(Arg.Any<object>());
            idleControllerStateMock_.Received(1).OnStateSelected();
        }   
        
        [Test]
        public void Construction_OnTriggerPressed_first_time()
        {
            carrierControllerStateMock_.Identifier.Returns("Carrier");
            stateAssignerMock_.Assign(Arg.Any<object>()).Returns(carrierControllerStateMock_);
            
            var unused = new Controller(stateAssignerMock_, controllerBehaviourMock_, indicatorBehaviourMock_, waveformInterpolationBehaviourMock_);

            {
                controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();

                stateAssignerMock_.Received(1).Assign(Arg.Any<object>());
                indicatorBehaviourMock_.Received(1).FuncText = "Carrier";
                carrierControllerStateMock_.Received(1).OnStateSelected();
            }
        }

        [Test]
        public void Construction_OnTriggerPressed_second_time_same_state()
        {
            carrierControllerStateMock_.Identifier.Returns("Carrier");
            stateAssignerMock_.Assign(Arg.Any<object>()).Returns(carrierControllerStateMock_);

            var unused = new Controller(stateAssignerMock_, controllerBehaviourMock_, indicatorBehaviourMock_, waveformInterpolationBehaviourMock_);

            {
                controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();
                controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();

                stateAssignerMock_.Received(2).Assign(Arg.Any<object>());
                carrierControllerStateMock_.Received(2).OnStateSelected();
            }
        }

        [Test]
        public void Construction_OnTriggerPressed_second_time_different_state()
        {
            carrierControllerStateMock_.Identifier.Returns("Carrier", "Modulator");
            stateAssignerMock_.Assign(Arg.Any<object>())
                .Returns(carrierControllerStateMock_, modulatorControllerStateMock_);

            var unused = new Controller(stateAssignerMock_, controllerBehaviourMock_, indicatorBehaviourMock_, waveformInterpolationBehaviourMock_);

            {
                controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();
                controllerBehaviourMock_.TriggerPressed += Raise.Event<Action>();

                stateAssignerMock_.Received(2).Assign(Arg.Any<object>());
                carrierControllerStateMock_.Received(1).OnStateSelected();
            }
        }

        [Test]
        public void Construction_OnTriggerReleased()
        {
            var unused = new Controller(stateAssignerMock_, controllerBehaviourMock_, indicatorBehaviourMock_, waveformInterpolationBehaviourMock_);

            controllerBehaviourMock_.TriggerReleased += Raise.Event<Action>();

            stateAssignerMock_.Received(2).Unassign(Arg.Any<object>());
        }
    }
}