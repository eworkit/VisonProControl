using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Utilities.ExMethod;
namespace Utilities.Threadx
{
    /// <summary>
    /// 提供多线程操作队列的类
    /// 对队列的Action将按入队顺序依次执行
    /// </summary>
    public class ThreadQueue
    {
        protected System.Collections.Concurrent.ConcurrentQueue<Action> ActionQueues = new System.Collections.Concurrent.ConcurrentQueue<Action>();
        protected bool IsCancel { get; set; }   
        protected bool IsStop { get; set; }
        protected static readonly Object mu = new Object();
        protected System.Threading.ManualResetEvent Event = new System.Threading.ManualResetEvent(true);
        protected System.Threading.Thread Thread;
        public event EventHandler Completed;
        public int MaxQueueCount { get; set; }
        public ThreadQueue()
        {
            MaxQueueCount = 0;
            IsCancel = false;
            IsStop = true;
        }      
        public virtual void Enqueue(Action act)
        {
            if (act == null)
                return;
            lock (mu)
            {
                ActionQueues.Enqueue(act);
                if(MaxQueueCount>0)
                {
                    
                }
              //  Event.Set();
                Start();
            }
        }   
        public virtual void Start()
        {
            lock (mu)
            {
                if (!IsStop)
                    return;
                IsCancel = false;
                IsStop = false;
                Thread = new System.Threading.Thread(Work) { IsBackground = true };
                Thread.Start();
            }
        }
        protected virtual void Work()
        {
            while (!IsCancel)
            { 
              //  Event.WaitOne();
                Action act;
                if (ActionQueues.TryDequeue(out act))
                {
                    if (IsCancel)
                        break;
                    if (act != null)
                        act();
                }
                else
                {
                   //// Event.Reset();
                    break;
                }
            }
         //   Event.Reset();
            IsStop = true;
            OnCompleted(this, EventArgs.Empty);
        }
        public virtual void Cancel()
        {
            IsCancel = true;
            Event.Set();
            Action act;
            while (ActionQueues.TryDequeue(out act)) ;
        }
        protected virtual void OnCompleted(object sender, EventArgs e)
        {
            if (Completed != null)
                Completed(sender, e);
        }
    }
}
