using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
namespace Utilities.Threadx
{
   public   class TimerQueue
    {
        System.Collections.Concurrent.ConcurrentQueue<Action> Queues = new System.Collections.Concurrent.ConcurrentQueue<Action>();
        bool IsCancel = false;
        bool IsStop = true;
        static readonly Object mu = new Object();
        System.Threading.ManualResetEvent Event = new System.Threading.ManualResetEvent(true);
        Timer QuTimer = new Timer(); 
       public TimerQueue(int interval)
        {
            QuTimer.Interval = interval;
            QuTimer.Elapsed += QuTimer_Elapsed;
        }

       void QuTimer_Elapsed(object sender, ElapsedEventArgs e)
       {
           Work();
       }
       void Work()
       {
           while (!IsCancel)
           {               
               Event.WaitOne();
               Action act;
               if (Queues.TryDequeue(out act))
               {
                   if (IsCancel)
                       break;
                   if (act != null)
                       act();
               }
               else
               { 
                   break;
               }
           }
           Event.Reset();
           IsStop = true; 
       }
        public void Enqueue(Action act)
        {
            if (act == null)
                return;
            lock (mu)
            {
                Queues.Enqueue(act);
                Event.Set();
                Start();
            }
        }
        public void Start()
        {
            lock (mu)
            {
                if (!IsStop)
                    return;
                IsCancel = false;
                IsStop = false;
                QuTimer.Start();
            }
        }
        public void Cancel()
        {
            IsCancel = true;
            Event.Set();
            ClearQueue();
        }
       public void ClearQueue()
        {
            Action act;
            while (Queues.TryDequeue(out act)) ;
        }
       public int Count { get { return Queues.Count; } }
    }
}
