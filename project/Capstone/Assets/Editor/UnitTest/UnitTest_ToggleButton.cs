using System;
using DomainF;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests
{
    public class UnitTest_ToggleButton
    {
        private IToggleButtonBehaviour toggleButtonBehaviour_;
        
        private ToggleButton toggleButton_;

        [SetUp]
        public  void SetUp()
        {
            toggleButtonBehaviour_ = Substitute.For<IToggleButtonBehaviour>();
            toggleButton_ = new ToggleButton(toggleButtonBehaviour_);
        }

        [Test]
        public void Construction_Toggle()
        {
            toggleButtonBehaviour_.ButtonTouched += Raise.Event<Action>();
            toggleButtonBehaviour_.ButtonTouched += Raise.Event<Action>();

            toggleButtonBehaviour_.Received(1).State = true;
            toggleButtonBehaviour_.Received(1).State = false;
        }

        [Test]
        public void Toggle()
        {
            toggleButton_.ButtonStateChanged += (state) =>
            {
                Assert.True(state);    
            };
            
            toggleButton_.Toggle();
            
            toggleButtonBehaviour_.Received(1).State = true;
        }
    }
}