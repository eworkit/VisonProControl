using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STh=System.Threading;
using IteUtils.ExMethod;
namespace IteUtils.Thread
{
    /// <summary>
    /// 提供多线程操作队列的类
    /// </summary>
    public class ThreadQueue
    {
        System.Collections.Concurrent.ConcurrentQueue<Action> Queues = new System.Collections.Concurrent.ConcurrentQueue<Action>();
        bool IsCancel = false;
        bool IsStop = true;
        static readonly Object mu = new Object();
        STh.ManualResetEvent Event = new STh.ManualResetEvent(true);
        STh.Thread Thread;
        public event EventHandler Completed;
        public ThreadQueue()
        {
            //
        }
        /// <summary>
        /// 将对象添加到队列尾部
        /// </summary>
        /// <param name="act"></param>
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
        /// <summary>
        /// 开启队列操作线程
        /// </summary>
        public void Start()
        {
            lock (mu)
            {
                if (!IsStop)
                    return;
                IsCancel = false;
                IsStop = false;
                Thread = new STh.Thread(Work) { IsBackground = true };
                Thread.Start();
            }
        }
        void Work()
        {
            while (true)
            {
                if (IsCancel)
                    break;
                Event.WaitOne();
                Action act;
                if (Queues.TryDequeue(out act))
                {
                    if (act != null)
                        act();
                }
                else
                {
                    Event.Reset();
                    break;
                }
            }
            Event.Reset();
            IsStop = true;
            Completed.Do(this, EventArgs.Empty);
        }
        public void Cancel()
        {
            IsCancel = true;
            Event.Set();
            Action act;
            while (Queues.TryDequeue(out act)) ;
        }
    }
}
