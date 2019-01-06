using System;

namespace DomainF
{
    public interface IPureDataFacade
    {
        void SendMessage(string message, float value);
        void SendMessage<T>(string message, T[] values);

        event Action<float> LevelChanged ;
    }

    public class PureDataFacade : IPureDataFacade
    {
        private float[] singleElementArray_ = {0f};
        
        public PureDataFacade()
        {
            PureData.OpenPatch("main");
            PureData.Receive("level", OnLevelReceived );
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

        private void OnLevelReceived(float level)
        {
            if (LevelChanged != null) LevelChanged.Invoke(level);
        }
        
        public event Action<float> LevelChanged;

    }
}