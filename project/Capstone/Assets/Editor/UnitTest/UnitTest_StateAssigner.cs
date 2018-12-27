using DomainF;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_StateAssigner
    {        
        private IControllerState carrierStateMock_, modulatorStateMock_;
        private IControllerStateFactory controllerStateFactoryMock_;
        
        private StateAssigner stateAssigner_;

        
        [SetUp]
        public void SetUp()
        {
            controllerStateFactoryMock_ = Substitute.For<IControllerStateFactory>();
            controllerStateFactoryMock_.CreateCarrierState().Returns(carrierStateMock_);
            controllerStateFactoryMock_.CreateModulatorState().Returns(modulatorStateMock_);

            stateAssigner_ = new StateAssigner(controllerStateFactoryMock_);
        }

        [Test]
        public void Construction()
        {
            controllerStateFactoryMock_.Received(1).CreateCarrierState();
            controllerStateFactoryMock_.Received(1).CreateModulatorState();
        }

        [Test]
        public void Assign_carrier_controller_is_null()
        {
            var returnedState = stateAssigner_.Assign(Arg.Any<object>());
            Assert.AreEqual(returnedState, carrierStateMock_);
        }

        [Test]
        public void Assign_carrier_controller_is_not_null()
        {
            var unused = stateAssigner_.Assign(Arg.Any<object>());
            var returnedState = stateAssigner_.Assign(Arg.Any<object>());
            Assert.AreEqual(returnedState, modulatorStateMock_);
        }
        
        [Test]
        public void Unassign()
        {
            object obj = new object();
            var unused = stateAssigner_.Assign(obj);
            stateAssigner_.Unassign(obj);
            
            var returnedState = stateAssigner_.Assign(obj);
            Assert.AreEqual(returnedState, carrierStateMock_);
        }
        
        [Test]
        public void Unassign_with_different_obj()
        {
            object obj1 = new object();
            object obj2 = new object();

            var unused = stateAssigner_.Assign(obj1);
            stateAssigner_.Unassign(obj2); // doesn't work
            
            var returnedState = stateAssigner_.Assign(obj1);
            Assert.AreEqual(returnedState, modulatorStateMock_);
        }
    }
}