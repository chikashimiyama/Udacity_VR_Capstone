using System;
using DomainF;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests
{
    public class UnitTest_OutputWaveform
    {
        private IOutputWaveformBehaviour outputWaveformBehaviourMock_;
        private IPureDataArrayFacade outputArrayFacadeMock_;
        private float[] dummySamples_;        
        
        [SetUp]
        public void SetUp()
        {
            outputWaveformBehaviourMock_ = Substitute.For<IOutputWaveformBehaviour>();
            outputArrayFacadeMock_ = Substitute.For<IPureDataArrayFacade>();
            dummySamples_ = new float[512];
            outputArrayFacadeMock_.Get().Returns(dummySamples_);

            var unused = new OutputWaveform(outputWaveformBehaviourMock_, outputArrayFacadeMock_);
        }

        [Test]
        public void Construction_OnUpdated()
        {
            outputWaveformBehaviourMock_.Updated += Raise.Event<Action>();

            outputArrayFacadeMock_.Received(1).Get();
            outputWaveformBehaviourMock_.Received(1).DrawWaveform(dummySamples_);
        }
        
    }
}