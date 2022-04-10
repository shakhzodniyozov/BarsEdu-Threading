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

    Thread thread = new Thread(() =>
    {
        try
        {
            var response = new DummyRequestHandler().HandleRequest(message!, arguments.ToArray());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"<-- Сообщение с идентификатором {Thread.CurrentThread.ManagedThreadId} получил ответ - {response}.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Сообщение с идентификатором {Thread.CurrentThread.ManagedThreadId} упало. {e.Message}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    });

    thread.IsBackground = true;
    thread.Start();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"--> Было отправлено сообщение '{message}'. Присвоен идентификатор {thread.ManagedThreadId}");
    Console.ForegroundColor = ConsoleColor.Gray;
}