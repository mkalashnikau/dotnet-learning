using System;
using System.Xml;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> list = new DoublyLinkedList<T>();

        public T Dequeue()
        {
            if (list.Length == 0)
                throw new InvalidOperationException();

            return list.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            list.Add(item);
        }

        public T Pop()
        {
            if (list.Length == 0)
                throw new InvalidOperationException();

            return list.RemoveAt(list.Length - 1);
        }

        public void Push(T item)
        {
            list.Add(item);
        }
    }
}
