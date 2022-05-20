namespace TCP_Client
{
    class Program
    {


        static async Task Main(string[] args)
        {
            string ServerIP = "127.0.0.1";
            int Port = 13000;

            var cancellationToken = new CancellationTokenSource();
            UserSpace userSpace = new UserSpace();
            Client client = new Client();
            GetAPi getAPi = new GetAPi();
            CollectData collectData = new CollectData();
            var task = userSpace.StartConsoleInputHandlerAsync(cancellationToken);
            userSpace.ExecuteWhenCommandAppears("Start", () => client.StartAsync(ServerIP, Port));
            userSpace.ExecuteWhenCommandAppearsMessage("Send", (message) => client.SendMessage(message));
            userSpace.ExecuteWhenCommandAppears("USDPLN", () => getAPi.ShowAsync());
            userSpace.ExecuteWhenCommandAppears("Data", () => collectData.ShowList());
            userSpace.ExecuteWhenCommandAppears("Timer", () => TimerUse.WaitForAsync(1, () => Console.WriteLine($"Time now: {DateTime.Now}")));
            userSpace.ExecuteWhenCommandAppears("exit", () =>
            {
                cancellationToken.Cancel();
                client.Stop();
            });
            
            await task;
            Console.WriteLine("End of application");
        }
    }
}