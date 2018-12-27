namespace DomainF
{
    public interface IPureDataFacade
    {
        void SendMessage<T>(string message, T[] values);
    }

    public class PureDataFacade : IPureDataFacade
    {
        public PureDataFacade()
        {
            PureData.OpenPatch("main");
        }

        public void SendMessage<T>(string message, T[] values)
        {
            PureData.SendMessage<T>("toPd", message, values);
        }
    }
}