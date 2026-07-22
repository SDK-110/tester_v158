using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.useful
{
    class CircularArray<T>
    {
        private List<T> array;
        private int capacity;
        private int head;
        private int tail;

        public CircularArray(int capacity)
        {
            this.capacity = capacity;
            this.array = new List<T>(capacity);
            this.head = 0;
            this.tail = 0;
        }

        public void Enqueue(T item)
        {
            if (array.Count < capacity)
            {
                array.Add(item);
                tail = (tail + 1) % capacity;
            }
            else
            {
                array[head] = item;
                head = (head + 1) % capacity;
                tail = (tail + 1) % capacity;
            }
        }

        public void clear() {


            this.capacity = capacity;
            this.array = new List<T>(capacity);
            this.head = 0;
            this.tail = 0;
           


        }
 
        public List<T> get_array {

         get { return array; }  
   
        }
    }
}
