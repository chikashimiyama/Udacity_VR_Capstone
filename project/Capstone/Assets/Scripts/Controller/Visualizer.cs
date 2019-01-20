using UnityEngine;

namespace DomainF
{
    public class Visualizer
    {
        private readonly IOutputWaveformBehaviour outputWaveformBehaviour_;
        private readonly IVisualizerBehaviour fftCircleBehaviour_;
        private readonly IPureDataFacade pureDataFacade_;
        
        private readonly IPureDataArrayFacade outputArrayFacade_;
        private readonly IPureDataArrayFacade fftArrayFacade_;
        private readonly float[] revLevel_;
        
        public Visualizer(IOutputWaveformBehaviour outputWaveformBehaviour,
            IVisualizerBehaviour fftCircleBehaviour,
            IPureDataFacade pureDataFacade,
            IPureDataArrayFacade outputArrayFacade = null,
            IPureDataArrayFacade fftArrayFacade = null)
        {
            revLevel_ = new float[1];
            fftCircleBehaviour_ = fftCircleBehaviour;
            outputWaveformBehaviour_ = outputWaveformBehaviour;
            pureDataFacade_ = pureDataFacade;
            outputArrayFacade_ = outputArrayFacade ?? new PureDataArrayFacade("output", 512);
            fftArrayFacade_ = fftArrayFacade ?? new PureDataArrayFacade("fft", 512);
            
            outputWaveformBehaviour_.Updated += OnWaveformUpdated;
            fftCircleBehaviour_.Updated += OnSpectrumUpdated;
        }

        private void OnWaveformUpdated()
        {
            var samples = outputArrayFacade_.Get();
            outputWaveformBehaviour_.Visualize(samples);
            
            var distance = outputWaveformBehaviour_.Distance;
            revLevel_[0] = Mathf.Clamp(distance, 0f, 20f) / 20f;
            pureDataFacade_.SendMessage("rev", revLevel_);
        }

        private void OnSpectrumUpdated()
        {
            var samples = fftArrayFacade_.Get(); // the value is not square rooted due to bug
            for (var i = 0; i < samples.Length; i++)
                samples[i] = Mathf.Sqrt(samples[i]) / 50f; // scale down
            fftCircleBehaviour_.Visualize(samples);
        }
    }
}