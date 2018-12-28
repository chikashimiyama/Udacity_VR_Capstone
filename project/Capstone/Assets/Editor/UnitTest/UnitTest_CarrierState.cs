using DomainF;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_CarrierState
    {
        private IPureDataFacade pureDataFacadeMock_;
        private CarrierState carrierState_;
        
        [SetUp]
        public void SetUp()
        {
            pureDataFacadeMock_ = Substitute.For<IPureDataFacade>();
            carrierState_ = new CarrierState(pureDataFacadeMock_);
        }

        [Test]
        public void OnStateSelected()
        {
            carrierState_.OnStateSelected();

            pureDataFacadeMock_.Received(1).SendMessage("car_active", Arg.Do<float[]>(arg =>
            {
                Assert.AreEqual(arg[0], 1.0f);
            }));
        }

        [Test]
        public void OnStateDeselected()
        {
            carrierState_.OnStateDeselected();

            pureDataFacadeMock_.Received(1).SendMessage("car_active", Arg.Do<float[]>(arg =>
            {
                Assert.AreEqual(arg[0], 0.0f);
            }));
        }
    }
}