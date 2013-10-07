namespace chui.SignalR
{
    public interface IClientMessenger
    {
        void SendMessage(string dispatchId, string message);
    }
}