using DomainF;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

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
            modulatorState_.OnStateSelected();

            pureDataFacadeMock_.Received(1).SendMessage("mod_active", 1.0f);
        }

        [Test]
        public void OnStateDeselected()
        {
            modulatorState_.OnStateDeselected();

            pureDataFacadeMock_.Received(1).SendMessage("mod_active", 0.0f);
        }

        [Test]
        public void OnDistanceChanged()
        {
            modulatorState_.OnDistanceChanged(20f);

            pureDataFacadeMock_.Received(1).SendMessage("mod_amp", 500f);
        }

        [Test]
        public void OnTransformChanged()
        {
            var go = new GameObject();
            go.transform.eulerAngles = new Vector3(20f, 0f, 0f);
            modulatorState_.OnTransformChanged(go.transform);
            
            pureDataFacadeMock_.Received(1).SendMessage("mod_freq", 388.8889f);
        }
    }
}