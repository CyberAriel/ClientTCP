using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TCP_Client
{


    internal static class TimerUse
    {
        private static readonly System.Timers.Timer timer = new System.Timers.Timer(1000);

        static TimerUse() => timer.Start();



        public static void WaitForAsync(int ms, Action toDo)
        {
            
            
           
            void Timer_Elapsed(object? sender, ElapsedEventArgs e)
            {
                Console.WriteLine($"static: { ms}");
                if (--ms == 0)
                {
                    toDo?.Invoke();
                    timer.Elapsed -= Timer_Elapsed;
                   
                }
            }

            timer.Elapsed += Timer_Elapsed;
            
        }
    }

}