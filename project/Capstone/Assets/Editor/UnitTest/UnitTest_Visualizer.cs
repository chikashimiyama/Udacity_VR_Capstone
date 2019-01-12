using System;
using DomainF;
using NUnit.Framework;
using NSubstitute;

namespace UnitTests
{
    public class UnitTest_Visualizer
    {
        private IOutputWaveformBehaviour outputWaveformBehaviourMock_;
        private IVisualizerBehaviour fftCircleBehaviourMock_;
        private IPureDataFacade pureDataFacadeMock_;
        private IPureDataArrayFacade outputArrayFacadeMock_;
        private IPureDataArrayFacade spectrumArrayFacadeMock_;
        private float[] dummySamples_;        
        
        [SetUp]
        public void SetUp()
        {
            pureDataFacadeMock_ = Substitute.For<IPureDataFacade>();
            outputWaveformBehaviourMock_ = Substitute.For<IOutputWaveformBehaviour>();
            fftCircleBehaviourMock_ = Substitute.For<IVisualizerBehaviour>();
            outputArrayFacadeMock_ = Substitute.For<IPureDataArrayFacade>();
            spectrumArrayFacadeMock_ = Substitute.For<IPureDataArrayFacade>();
            
            dummySamples_ = new float[512];
            outputArrayFacadeMock_.Get().Returns(dummySamples_);
            spectrumArrayFacadeMock_.Get().Returns(dummySamples_);            
            
            outputWaveformBehaviourMock_.Distance.Returns(10f);

            var unused = new Visualizer(outputWaveformBehaviourMock_, fftCircleBehaviourMock_, 
                pureDataFacadeMock_, outputArrayFacadeMock_, spectrumArrayFacadeMock_);
        }

        [Test]
        public void Construction_OnWaveformUpdated()
        {
            outputWaveformBehaviourMock_.Updated += Raise.Event<Action>();

            outputArrayFacadeMock_.Received(1).Get();
            outputWaveformBehaviourMock_.Received(1).Visualize(dummySamples_);
            pureDataFacadeMock_.Received(1).SendMessage("rev", Arg.Any<float[]>());
        }
        
        [Test]
        public void Construction_OnSpectrumUpdated()
        {
            fftCircleBehaviourMock_.Updated += Raise.Event<Action>();

            spectrumArrayFacadeMock_.Received(1).Get();
            fftCircleBehaviourMock_.Received(1).Visualize(dummySamples_);
        }
    }
}