using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hw_21._11._2023_own_generic_class
{
    public class PriorityQueueElement<TElement, TPriority> where TPriority
        : IComparable<TPriority>
    {
        public TElement Element { get; }
        public TPriority Priority { get; }

        public PriorityQueueElement(TElement item, TPriority priority)
        {
            Element = item;
            Priority = priority;
        }
    }

    public class MyPriorityQueue<TElement, TPriority> where TPriority
        : IComparable<TPriority> //Это уточнение чтоб не происходил лишний раз boxing.
    {
        List<PriorityQueueElement<TElement, TPriority>> PriorityQueue;
        public MyPriorityQueue()
        {
            PriorityQueue = new List<PriorityQueueElement<TElement, TPriority>>();
        }

        int count = 0;
        int Count { get { return count; } }

        public bool IsEmpty { get { return Count == 0; } }

        public void Enqueue(TElement item, TPriority priority)
        {
            var element = new PriorityQueueElement<TElement, TPriority>(item, priority);
            int i;
            for (i = 0; i < Count; i++)
            {
                if (PriorityQueue[i].Priority.CompareTo(element.Priority) > 0)
                {
                    PriorityQueue.Insert(i, element);
                    break;
                }
            }
            if (i == Count)
                PriorityQueue.Insert(Count, element);
            count++;
        }

        public PriorityQueueElement<TElement, TPriority> Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("PriorityQueue is empty");

            var element = PriorityQueue[0];
            PriorityQueue.RemoveAt(0);
            return element;
        }

        public override string ToString()
        {
            string ALLELEMENTS = "";
            foreach(var element in PriorityQueue)
            {
                ALLELEMENTS += $"ELEMENT: {element.Element} PRIORITY: {element.Priority} \n";
            }

            return ALLELEMENTS;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            MyPriorityQueue<int, int> Queue = new MyPriorityQueue<int, int>();
            Queue.Enqueue(2, 4);
            Queue.Enqueue(4, 4);
            Queue.Enqueue(4,9);
            Queue.Enqueue(9, 9);
            Queue.Enqueue(8, 8);
            Queue.Enqueue(1, 4);
            Queue.Enqueue(8, 8);
            Queue.Enqueue(83, 49);
            Queue.Enqueue(1, 1);

            Console.WriteLine(Queue);

            Queue.Dequeue();

            Console.WriteLine(Queue);

        }
    }
}
