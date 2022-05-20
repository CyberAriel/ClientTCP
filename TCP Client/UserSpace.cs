
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Client
{
    internal class UserSpace
    {
        private delegate void ReceivedLine(string line);
        private event ReceivedLine OnReceivedLine;

        public void ExecuteWhenCommandAppearsMessage(string command, Action<string> callback)
        {
            void Menu_OnReceivedLineWithMessage(string line)
            {
               
                if (line.StartsWith(command))
                {
                    string tmp = line.Replace(command, "").Trim();
                    callback?.Invoke(tmp);
                }

            }
            OnReceivedLine += Menu_OnReceivedLineWithMessage;
        }

        public void ExecuteWhenCommandAppears(string command, Action callback)
        {
            void Menu_OnReceivedLine(string line)
            {
                if (line.Equals(command, StringComparison.InvariantCultureIgnoreCase))
                {
                    //OnReceivedLine -= Menu_OnReceivedLine;
                    callback?.Invoke();
                }
            }
            OnReceivedLine += Menu_OnReceivedLine;
        }

        public Task WaitForCommandToAppearAsync(string command)
        {
            var tcs = new TaskCompletionSource();

            void Menu_OnReceivedLine(string line)
            {
                if (line.Equals(command, StringComparison.InvariantCultureIgnoreCase))
                {
                    OnReceivedLine -= Menu_OnReceivedLine;
                    tcs.TrySetResult();
                }
            }

            OnReceivedLine += Menu_OnReceivedLine;
            return tcs.Task;
        }

        public Task StartConsoleInputHandlerAsync(CancellationTokenSource cancellationToken)
        {
            
            return Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    
                    string line = Console.ReadLine();
                    OnReceivedLine?.Invoke(line);
                }
            });
        }
    }

}
