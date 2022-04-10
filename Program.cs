while (true)
{
    Console.WriteLine("Введите текст запроса для отправки. Введите /q для выхода.");

    var message = Console.ReadLine();

    if (message == "/q")
        break;

    var arguments = new List<string>();

    while (true)
    {
        Console.WriteLine("Введите аргумент сообщения. Если аргумент не нужен - введите /end");
        var argument = Console.ReadLine();

        if (argument == "/end")
            break;
        else
            arguments.Add(argument!);
    }

    ThreadPool.QueueUserWorkItem(state =>
    {
        var messageId = Guid.NewGuid();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"--> Было отправлено сообщение '{message}'. Присвоен идентификатор {messageId}");
        Console.ForegroundColor = ConsoleColor.Gray;

        try
        {
            var response = new DummyRequestHandler().HandleRequest(message!, arguments.ToArray());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"<-- Сообщение с идентификатором {messageId} получил ответ - {response}.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Сообщение с идентификатором {messageId} упало. {e.Message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    });
}