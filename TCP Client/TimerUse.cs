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



        public static void WaitForAsync(String action, Action<DateTime> toDo)
        {
            
            
           
            void Timer_Elapsed(object? sender, ElapsedEventArgs e)
            {
                if (action.Equals("Stop"))
                {
                    Console.WriteLine("End of timer");
                    timer.Elapsed -= Timer_Elapsed;
                }
                DateTime date = DateTime.Now;
                
                    toDo?.Invoke(date);

                





            }
           
                timer.Elapsed += Timer_Elapsed;
            
           
            
        }

    }

}