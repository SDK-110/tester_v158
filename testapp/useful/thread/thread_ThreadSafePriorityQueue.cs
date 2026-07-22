using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

    namespace testapp.thread
{




        public class ThreadSafePriorityQueue<T>
        {
            private readonly SortedDictionary<int, ConcurrentQueue<T>> _queues = new SortedDictionary<int, ConcurrentQueue<T>>();
            private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

            public void Enqueue(T item, int priority)
            {
                _lock.EnterWriteLock();
                try
                {
                    if (!_queues.TryGetValue(priority, out var queue))
                    {
                        queue = new ConcurrentQueue<T>();
                        _queues[priority] = queue;
                    }
                    queue.Enqueue(item);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }

            public bool TryDequeue(out T item)
            {
                item = default(T);
                _lock.EnterUpgradeableReadLock();
                try
                {
                    if (_queues.Count == 0)
                        return false;

                    var highestPriority = _queues.Keys.Max();
                    var queue = _queues[highestPriority];

                    if (queue.TryDequeue(out item))
                    {
                        if (queue.IsEmpty)
                        {
                            _lock.EnterWriteLock();
                            try
                            {
                                _queues.Remove(highestPriority);
                            }
                            finally
                            {
                                _lock.ExitWriteLock();
                            }
                        }
                        return true;
                    }
                }
                finally
                {
                    _lock.ExitUpgradeableReadLock();
                }
                return false;
            }
        }
    }


