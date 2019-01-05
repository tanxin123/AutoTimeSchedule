using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTimeSchedule
{
    public class TimeHandle
    {

        private bool IsRunning { get; set; } = true;

        public void StopSchedule()
        {
            IsRunning = false;
        }

        private Action CallBackFunc { get; set; }
        public void SetAction(Action callBackFunc)
        {
            this.CallBackFunc = callBackFunc;
        }


        //public Action<object>  CallBackFunc { get; set; }

        
        private  void StartScheduleLoop(TimeSpan timeSpan)
        {
            if (IsRunning)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(timeSpan);
                    try
                    {
                        CallBackFunc();
                        StartScheduleLoop(timeSpan);
                    }
                    catch (Exception)
                    {

                    }

                });
            }
           
        }

        private void StartScheduleLoopWith24(TimeSpan timeSpan)
        {
            if (IsRunning)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(timeSpan);
                    try
                    {
                        CallBackFunc();
                        StartScheduleLoop(new TimeSpan(24,0,0));
                    }
                    catch (Exception)
                    {

                    }

                });
            }

        }

        public  bool StartTimeSpanSchedule(int seconds)
        {
            if (seconds==0)
            {
                return false;
            }

            try
            {
                StartScheduleLoop(new TimeSpan(0, 0, seconds));
            }
            catch (Exception)
            {
            }

            return true;
        }

        /// <summary>
        /// 每天几点执行
        /// </summary>
        /// <param name="timeString">时间参数 例如 12:00</param>
        /// <param name="CallBackFunc">回调函数</param>
        /// <returns></returns>
        public  bool StartTimeSpanSchedule(string timeString)
        {
            if (string.IsNullOrWhiteSpace(timeString))
            {
                return false;
            }
            string[] split = timeString.Split(':');
            if (split.Length != 2)
            {
                return false;
            }
            try
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = new TimeSpan(int.Parse(split[0]), int.Parse(split[1]), 0);
                if (now.TimeOfDay <= timeSpan)
                {
                    StartScheduleLoopWith24(timeSpan - now.TimeOfDay);
                }
                else
                {
                    StartScheduleLoopWith24(new TimeSpan(24, 0, 0) - (now.TimeOfDay - timeSpan));
                }
            }
            catch (Exception)
            {
            }

            return true;
        }
    }
}
