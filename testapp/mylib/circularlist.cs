using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.mylib
{
    public class CircularList<T> : List<T>
    {
        private int _head;
        private int _tail;
      
        public CircularList(int capacity)
            : base(capacity)
        {
            _head = 0;
            _tail = 0;
        }

        public void Add(T item)
        {
            base.Add(item);
            _tail = (_tail + 1) % Capacity;
        }

        public T Get(int index)
        {
            index = (_head + index) % Capacity;
            return base[index];
        }

        public void Set(int index, T value)
        {
            index = (_head + index) % Capacity;
            base[index] = value;
        }

        public T Remove()
        {
            T item = base[_head];
            base.RemoveAt(_head);
            _head = (_head + 1) % Capacity;
            return item;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }
    }
}
