using NUnit.Framework;
using NSubstitute;
using DomainF;


namespace UnitTests
{
    [TestFixture]
    public class UnitTest_ComponentFactory
    {
        private ComponentFactory componentFactory_;
        
        private IToggleButtonBehaviour toggleButtonBehaviour_;

        [SetUp]
        public void SetUp()
        {
            componentFactory_ = new ComponentFactory();
        }

        [Test]
        public void CreateToggleButton()
        {
            toggleButtonBehaviour_ = Substitute.For<IToggleButtonBehaviour>();
            var toggleButton = componentFactory_.CreateToggleButton(toggleButtonBehaviour_);
            Assert.True(toggleButton != null);            
        }
    }
}