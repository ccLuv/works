using System;
using System.Threading;

class Program
{
    public class AlarmClock
    {
        private DateTime _alarmTime;
        private bool _isRunning = true;

        public AlarmClock(DateTime alarmTime)
        {
            _alarmTime = alarmTime;
        }

        public void Start()
        {
            while (_isRunning)
            {
                Console.WriteLine("滴答...滴答...");
                if (DateTime.Now >= _alarmTime)
                {
                    Console.WriteLine("闹钟响铃了！请起床！");
                    _isRunning = false;
                }
                Thread.Sleep(1000); // 每秒检查一次
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }

    static void Main(string[] args)
    {
        DateTime alarmTime = DateTime.Now.AddSeconds(5);
        var alarmClock = new AlarmClock(alarmTime);

        Console.WriteLine($"闹钟已设置，将在 {alarmTime} 时响铃。");
        Console.WriteLine("按任意键停止闹钟...");

        // 启动闹钟
        var alarmThread = new Thread(alarmClock.Start);
        alarmThread.Start();

        // 等待用户按键
        Console.ReadKey();

        // 停止闹钟
        alarmClock.Stop();
        alarmThread.Join(); // 确保线程结束
        Console.WriteLine("闹钟已停止。");
    }
}