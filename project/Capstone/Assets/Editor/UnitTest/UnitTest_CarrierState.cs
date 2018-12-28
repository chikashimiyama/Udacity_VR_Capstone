using DomainF;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

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

            pureDataFacadeMock_.Received(1).SendMessage("car_active", 1f);
        }

        [Test]
        public void OnStateDeselected()
        {
            carrierState_.OnStateDeselected();

            pureDataFacadeMock_.Received(1).SendMessage("car_active", 0f);
        }

        [Test]
        public void OnDistanceChanged()
        {
            carrierState_.OnDistanceChanged(20f);

            pureDataFacadeMock_.Received(1).SendMessage("car_amp",0.5f);
        }
        
        [Test]
        public void OnPoseUpdated()
        {
            var transform = new GameObject().transform;
            transform.rotation = Quaternion.Euler(100f, 0f, 0f);
            pureDataFacadeMock_.Received(1).SendMessage("car_freq",100f);
        }
    }
}