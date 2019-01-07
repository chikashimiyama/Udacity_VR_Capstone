using UnityEngine;

namespace DomainF
{
    public class OutputWaveform
    {
        private readonly IOutputWaveformBehaviour outputWaveformBehaviour_;
        private readonly IPureDataFacade pureDataFacade_;
        private readonly IPureDataArrayFacade outputArrayFacade_;
        private readonly float[] revLevel_;
        
        public OutputWaveform(IOutputWaveformBehaviour outputWaveformBehaviour,
            IPureDataFacade pureDataFacade,
            IPureDataArrayFacade outputArrayFacade = null)
        {
            revLevel_ = new float[1];
            outputWaveformBehaviour_ = outputWaveformBehaviour;
            pureDataFacade_ = pureDataFacade;
            outputArrayFacade_ = outputArrayFacade ?? new PureDataArrayFacade("output", 512);
               
            outputWaveformBehaviour_.Updated += OnUpdated;
        }

        private void OnUpdated()
        {
            var samples = outputArrayFacade_.Get();
            outputWaveformBehaviour_.DrawWaveform(samples);
            var distance = outputWaveformBehaviour_.Distance;
            revLevel_[0] = Mathf.Clamp(distance, 0f, 20f) / 20f;
            pureDataFacade_.SendMessage("rev", revLevel_);
        }
        
    }
}