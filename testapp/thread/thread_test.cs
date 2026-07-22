using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.thread
{
    using System;
    using System.Threading;

    class thread_test
    {
        private static Mutex mutex = new Mutex();

        static void test(string[] args)
        {
            ThreadPool.SetMaxThreads(10, 10);
            ThreadPool.QueueUserWorkItem(ThreadProc, "Thread 1");
            ThreadPool.QueueUserWorkItem(ThreadProc, "Thread 2");
            ThreadPool.QueueUserWorkItem(ThreadProc, "Thread 3");

            Console.ReadLine();
        }

        static void ThreadProc(object state)
        {
            string threadName = (string)state;

            if (mutex.WaitOne(0)) // 尝试获取互斥锁
            {
                try
                {
                    Console.WriteLine($"{threadName} acquired the mutex.");
                    // 模拟长时间运行的任务
                    Thread.Sleep(2000);
                }
                finally
                {
                    Console.WriteLine($"{threadName} releasing the mutex.");
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                Console.WriteLine($"{threadName} could not acquire the mutex and is exiting.");
            }
        }
    }
}
