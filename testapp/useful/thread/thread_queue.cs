using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.thread
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class CircularBuffer<T>
    {
        private readonly ConcurrentQueue<T> _queue;
        private readonly Queue<T> _innerQueue;
        private readonly int _capacity;

        // 构造函数
        public CircularBuffer(int capacity)
        {
            _capacity = capacity;
            _queue = new ConcurrentQueue<T>();
            _innerQueue = new Queue<T>();
        }

        // 添加元素到环形容器
        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
            if (_queue.Count > _capacity)
            {
                // 如果队列满了，移除最早的元素
                T removedItem;
                _queue.TryDequeue(out removedItem);
                // 同时更新_innerQueue，确保数据一致性
                T frontItem = _innerQueue.Dequeue();
                if (!EqualityComparer<T>.Default.Equals(frontItem, removedItem))
                {
                    throw new InvalidOperationException("Inner queue and concurrent queue are out of sync.");
                }
            }
        }

        // 从环形容器中移除元素
        public bool TryDequeue(out T item)
        {
            if (_queue.TryDequeue(out item))
            {
                // 同时更新_innerQueue
                _innerQueue.Dequeue();
                return true;
            }
            return false;
        }

        // 查看环形容器的元素数量
        public int Count => _queue.Count;

        // 检查环形容器是否为空
        public bool IsEmpty => _queue.IsEmpty;

        // 检查环形容器是否已满
        public bool IsFull => _queue.Count == _capacity;

        // 访问第一个元素（不移除）
        public T Peek()
        {
            if (_innerQueue.Count > 0)
            {
                return _innerQueue.Peek();
            }
            throw new InvalidOperationException("The buffer is empty.");
        }
    }

    class Program
    {
        static void Main()
        {
            CircularBuffer<int> buffer = new CircularBuffer<int>(3);

            buffer.Enqueue(1);
            buffer.Enqueue(2);
            Console.WriteLine(buffer.Peek()); // 输出 1
            buffer.Enqueue(3); // 此时队列满了，再添加
            buffer.Enqueue(4); // 1 将被移除

            while (buffer.TryDequeue(out int item))
            {
                Console.WriteLine(item);
            }
        }
    }
}
