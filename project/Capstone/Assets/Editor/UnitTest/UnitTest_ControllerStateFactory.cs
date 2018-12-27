using DomainF;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_ControllerStateFactory
    {
        private IControllerStateFactory ControllerStateFactory_;
        
        [SetUp]
        public void SetUp()
        {
            ControllerStateFactory_  = new ControllerStateFactory();
        }
        
        [Test]
        public void CreateCarrierState()
        {
            var carrierState = ControllerStateFactory_.CreateCarrierState(Arg.Any<IPureDataFacade>());
            Assert.True(carrierState != null);
            Assert.DoesNotThrow(()=>
            {
                var unused = (CarrierState) carrierState;
            });
        }
        
        [Test]
        public void CreateModulatorState()
        {
            var modulatorState = ControllerStateFactory_.CreateModulatorState(Arg.Any<IPureDataFacade>());
            Assert.True(modulatorState != null);
            Assert.DoesNotThrow(()=>
            {
                var unused = (ModulatorState) modulatorState;
            });
        }
    }

    
}