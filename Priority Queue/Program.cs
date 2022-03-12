using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace Priority_Queue
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            HeapedPriorityQueue priorityQueue = new HeapedPriorityQueue(size: 8);
            priorityQueue.Enqueue(new Task(12, "1st added"));
            priorityQueue.Enqueue(new Task(5, "2nd added"));
            priorityQueue.Enqueue(new Task(12, "3rd added"));
            priorityQueue.Enqueue(new Task(1, "4th added"));
            priorityQueue.Enqueue(new Task(1, "5th added"));
            priorityQueue.Enqueue(new Task(12, "6th added"));
            priorityQueue.Enqueue(new Task(5, "7th added"));
            priorityQueue.Enqueue(new Task(10, "8th added"));

            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();
            priorityQueue.Dequeue().Announce();

        }

        class Task
        {
            public int Priority;
            public string Title;
            public DateTime TimeAdded;

            public Task(int priority, string title)
            {
                Priority = priority;
                Title = title;
                TimeAdded = DateTime.Now;
            }

            public void Announce()
            {
                Console.WriteLine($"Task: {Title}, Priority: {Priority}");
            }
        }

        class HeapedPriorityQueue
        {
            private Task[] Queue;
            private int End;

            public HeapedPriorityQueue(int size)
            {
                Queue = new Task[size + 1];
                End = 0;
            }

            public void Enqueue(Task task)
            {
                if (IsFull())
                {
                }
                else
                {
                    Queue[End] = task;
                    SiftUp(End);
                    End += 1;

                }
            }

            public Task Dequeue()
            {
                if (!IsEmpty())
                {
                    Task temp = Queue[0];
                    Queue[0] = Queue[End - 1];
                    Queue[End - 1] = null;
                    End -= 1;
                    SiftDown(0);

                    return temp;
                }
                else
                {
                    return null;
                }
            }

            private int Parent(int i)
            {
                return (int) Math.Floor(Convert.ToDouble(i) - 1.0 / 2.0);
            }

            private int Left(int i)
            {
                return (i * 2) + 1;
            }

            private int Right(int i)
            {
                return (i * 2) + 2;
            }

            private bool IsEmpty()
            {
                if (End == 0)
                {
                    return true;
                }

                return false;
            }

            private bool IsFull()
            {
                if (End == Queue.Length)
                {
                    return true;
                }

                return false;
            }

            private void SiftDown(int i)
            {
                
                if (Left(i) < Queue.Length && Right(i) < Queue.Length)
                {
                    if (Queue[Left(i)] != null && Queue[Right(i)] != null)
                    {
                        int smallest;
                        if (Queue[Left(i)].Priority < Queue[Right(i)].Priority &&
                            (Queue[Left(i)].Priority < Queue[i].Priority | (Queue[Left(i)].Priority == Queue[i].Priority && DateTime.Compare(Queue[Left(i)].TimeAdded, Queue[i].TimeAdded) < 0)))
                        {
                            Task temp = Queue[i];
                            Queue[i] = Queue[Left(i)];
                            Queue[Left(i)] = temp;
                            SiftDown(Left(i));
                        }
                        else if (Queue[Right(i)].Priority < Queue[i].Priority)
                        {
                            Task temp = Queue[i];
                            Queue[i] = Queue[Right(i)];
                            Queue[Right(i)] = temp;
                            SiftDown(Right(i));
                        }
                    }
                }
            }

            private void SiftUp(int i)
            {
                if (Parent(i) >= 0 && i < Queue.Length)
                {
                    if (!(Queue[i] == null | Queue[Parent(i)] == null))
                    {
                        if (Queue[i].Priority < Queue[Parent(i)].Priority |
                            (Queue[Parent(i)].Priority == Queue[i].Priority &&
                             DateTime.Compare(Queue[Parent(i)].TimeAdded, Queue[i].TimeAdded) > 0))
                        {
                            Task temp = Queue[i];
                            Queue[i] = Queue[Parent(i)];
                            Queue[Parent(i)] = temp;

                            SiftUp(Parent(i));
                        }
                    }
                }
            }
            
        }
    }
}