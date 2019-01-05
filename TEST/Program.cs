using System;
using System.Threading;
using AutoTimeSchedule;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //TimeHandle.StartTimeSpanSchedule(3, () =>
            //{
            //    Console.WriteLine("{0} 456",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //});

            TimeHandle timeHandle = new TimeHandle();
            timeHandle.SetAction(()=> {
                Console.WriteLine("{0} 456", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            });
            timeHandle.StartTimeSpanSchedule(2);

            Thread.Sleep(20000);
            timeHandle.StopSchedule();
            Console.ReadKey();

        }
    }
}
