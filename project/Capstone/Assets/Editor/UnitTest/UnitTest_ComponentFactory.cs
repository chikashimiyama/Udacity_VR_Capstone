using NUnit.Framework;
using NSubstitute;
using DomainF;


namespace UnitTests
{
    [TestFixture]
    public class UnitTest_ComponentFactory
    {
        private IToggleButtonBehaviour toggleButtonBehaviourMock_;

        private ComponentFactory componentFactory_;

        [SetUp]
        public void SetUp()
        {
            componentFactory_ = new ComponentFactory();
        }

        [Test]
        public void CreateToggleButton()
        {
            toggleButtonBehaviourMock_ = Substitute.For<IToggleButtonBehaviour>();
            var toggleButton = componentFactory_.CreateToggleButton(toggleButtonBehaviourMock_);
            Assert.True(toggleButton != null);            
        }
    }
}