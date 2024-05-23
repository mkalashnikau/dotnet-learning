using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private class Node
        {
            public Node Prev { get; set; }
            public Node Next { get; set; }
            public T Value { get; set; }
        }

        private Node head;
        private Node tail;

        public int Length { get; private set; }

        public void Add(T e)
        {
            Node newNode = new Node { Value = e };
            if (tail != null)
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            else
            {
                head = tail = newNode;
            }
            Length++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length)
                throw new IndexOutOfRangeException();

            var newNode = new Node { Value = e };

            if (index == Length)
            {
                if (head == null)
                {
                    head = tail = newNode;
                }
                else
                {
                    tail.Next = newNode;
                    newNode.Prev = tail;
                    tail = newNode;
                }
            }
            else if (index == 0)
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            else
            {
                Node temp = head;
                for (int i = 0; i < index; i++)
                {
                    temp = temp.Next;
                }
                newNode.Next = temp;
                newNode.Prev = temp.Prev;
                temp.Prev.Next = newNode;
                temp.Prev = newNode;
            }

            Length++;
        }

        public T ElementAt(int index)
        {
            if (index >= Length || index < 0)
                throw new IndexOutOfRangeException();

            Node temp = head;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            return temp.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public void Remove(T item)
        {
            Node temp = head;
            while (temp != null)
            {
                if (temp.Value.Equals(item))
                {
                    if (temp.Prev != null)
                        temp.Prev.Next = temp.Next;
                    else
                        head = temp.Next;

                    if (temp.Next != null)
                        temp.Next.Prev = temp.Prev;
                    else
                        tail = temp.Prev;

                    Length--;
                    return;
                }
                temp = temp.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index >= Length || index < 0)
                throw new IndexOutOfRangeException();

            Node temp = head;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            T value = temp.Value;

            if (temp.Prev != null)
                temp.Prev.Next = temp.Next;
            else
                head = temp.Next;

            if (temp.Next != null)
                temp.Next.Prev = temp.Prev;
            else
                tail = temp.Prev;

            Length--;
            return value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
