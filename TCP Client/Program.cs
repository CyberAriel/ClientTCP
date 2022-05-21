using static TCP_Client.GetAPi;

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
            
            var task = userSpace.StartConsoleInputHandlerAsync(cancellationToken);
            userSpace.ExecuteWhenCommandAppears("Start", () => client.StartAsync(ServerIP, Port));
            userSpace.ExecuteWhenCommandAppearsMessage("Send", (message) => client.SendMessage(message));
            //userSpace.ExecuteWhenCommandAppears("USDPLN", (date) => getAPi.ShowAsync(date));
            userSpace.ExecuteWhenCommandAppears("Data", () => getAPi.ShowList());
            userSpace.ExecuteWhenCommandAppears("Value", () => getAPi.TakeValue());
            userSpace.ExecuteWhenCommandAppears("Timer Start", () => TimerUse.WaitForAsync("Start", (date) => getAPi.ShowAsync(date)));
            userSpace.ExecuteWhenCommandAppears("Timer Stop", () => TimerUse.WaitForAsync("Stop", (date) => getAPi.ShowAsync(date)));

            Console.WriteLine(DateTime.Now);


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