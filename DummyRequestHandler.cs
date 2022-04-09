public class DummyRequestHandler : IRequestHandler
{
    public string HandleRequest(string message, string[] arguments)
    {
        Thread.Sleep(10_000);
        if (message.Contains("упади"))
        {
            throw new Exception("Я упал, как сам просил");
        }
        return Guid.NewGuid().ToString("D");
    }
}