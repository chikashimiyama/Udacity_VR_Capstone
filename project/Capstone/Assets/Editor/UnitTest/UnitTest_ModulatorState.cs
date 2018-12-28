using DomainF;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UnitTest_ModulatorState
    {
        private IPureDataFacade pureDataFacadeMock_;
        private ModulatorState modulatorState_;
        
        [SetUp]
        public void SetUp()
        {
            pureDataFacadeMock_ = Substitute.For<IPureDataFacade>();
            modulatorState_ = new ModulatorState(pureDataFacadeMock_);
        }

        [Test]
        public void OnStateSelected()
        {
            modulatorState_.OnStateDeselected();

            pureDataFacadeMock_.Received(1).SendMessage("mod_active", Arg.Do<float[]>(arg =>
            {
               Assert.AreEqual(arg[0], 1.0f);
            }));
        }

        [Test]
        public void OnStateDeselected()
        {
            modulatorState_.OnStateDeselected();

            pureDataFacadeMock_.Received(1).SendMessage("mod_active", Arg.Do<float[]>(arg =>
            {
                Assert.AreEqual(arg[0], 0.0f);
            }));
        }
    }
}