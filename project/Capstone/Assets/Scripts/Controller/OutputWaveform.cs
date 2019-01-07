namespace DomainF
{
    public class OutputWaveform
    {
        private readonly IOutputWaveformBehaviour outputWaveformBehaviour_;
        private readonly IPureDataArrayFacade outputArrayFacade_;
        
        public OutputWaveform(IOutputWaveformBehaviour outputWaveformBehaviour,
            IPureDataArrayFacade outputArrayFacade = null)
        {
            outputWaveformBehaviour_ = outputWaveformBehaviour;
            outputArrayFacade_ = outputArrayFacade ?? new PureDataArrayFacade("output", 512);
               
            outputWaveformBehaviour_.Updated += OnUpdated;
        }

        private void OnUpdated()
        {
            var samples = outputArrayFacade_.Get();
            outputWaveformBehaviour_.DrawWaveform(samples);
        }
        
    }
}