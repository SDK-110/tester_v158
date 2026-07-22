using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testapp.thread
{

    class thread_pool_work
    {
        static async Task pool_work(string[] args)
        {
            int maxConcurrentThreads = 50;
            var throttle = new SemaphoreSlim(maxConcurrentThreads);

            // 任务列表
            var tasks = new Task[maxConcurrentThreads];
            for (int i = 0; i < maxConcurrentThreads; i++)
            {
                // 当信号量可用时，创建并启动任务
                await throttle.WaitAsync();
                tasks[i] = Task.Run(() =>
                {
                    try
                    {
                        // 执行任务
                        Console.WriteLine($"Task {Task.CurrentId} is running on thread {Thread.CurrentThread.ManagedThreadId}");
                        // 模拟任务执行时间
                        Thread.Sleep(1000);
                    }
                    finally
                    {
                        // 任务完成后释放信号量
                        throttle.Release();
                    }
                });
            }

            // 等待所有任务完成
            await Task.WhenAll(tasks);
        }
    }
}
