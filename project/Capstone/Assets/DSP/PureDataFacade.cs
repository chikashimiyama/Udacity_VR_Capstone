namespace DomainF
{
    public interface IPureDataFacade
    {
        void SendMessage(string message, float value);
        void SendMessage<T>(string message, T[] values);
    }

    public class PureDataFacade : IPureDataFacade
    {
        private float[] singleElementArray_ = {0f};
        
        public PureDataFacade()
        {
            PureData.OpenPatch("main");
        }

        public void SendMessage(string message, float value)
        {
            singleElementArray_[0] = value;
            SendMessage(message, singleElementArray_);
        }
        
        public void SendMessage<T>(string message, T[] values)
        {
            PureData.SendMessage("toPd", message, values);
        }
    }
}