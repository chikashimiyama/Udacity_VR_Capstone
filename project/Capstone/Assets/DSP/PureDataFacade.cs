namespace DomainF
{
	public interface IPureDataFacade
	{
		void SendMessage<T>(string receiveName, T value);
	}
	
	public class PureDataFacade : IPureDataFacade
	{	
		public PureDataFacade()
		{
			PureData.OpenPatch("main");
		}

		public void SendMessage<T>(string receiveName, T value)
		{
			PureData.Send(receiveName, value);
		}
	}
}
