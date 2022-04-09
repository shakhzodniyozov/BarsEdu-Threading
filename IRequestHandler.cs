internal interface IRequestHandler
{
    string HandleRequest(string message, string[] arguments);
}