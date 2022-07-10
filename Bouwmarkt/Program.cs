using System;

namespace Bouwmarkt 
{
    internal class Program
    {
        static void Main()
        {
            string[] inp  =  Console.ReadLine().Split();
            int nKassas = int.Parse(inp[0]);
            int nKlanten= int.Parse(inp[1]);
            string outputMode = inp[2];
            
            MinHeap<Kassa> kassas = new MinHeap<Kassa>();
            for (int i = 0; i < nKassas; i++)
                kassas.Insert(new Kassa(i, int.Parse(Console.ReadLine())));
            //todo: make a queue of klanten
            //when they arrive, extract smallest kassa for them to go to
            //add kassa back into heap

            // add events



            Console.ReadLine();
        }
        public class Kassa : IComparable<Kassa>, IHeapItem
        {
            public int KassaSpeed;
            public int pos;
            public int heapPos;
            public Queue<long> KassaQueue;

            public Kassa(int pos, int kassaSpeed)
            {
                KassaSpeed = kassaSpeed;
                this.pos = pos;
                KassaQueue = new Queue<long>();
            }

            public int CompareTo(Kassa other)
            {
                if (KassaQueue.Count < other.KassaQueue.Count)
                    return -1;
                if (KassaQueue.Count == other.KassaQueue.Count && pos < other.pos)
                    return -1;
                return 1;
            }

            public void SetHeapIndex(int index)
            {
                
            }
        }
        public class Event : IComparable<Event>, IHeapItem
        {

            public int CompareTo(Event other)
            {
                
            }

            public void SetHeapIndex(int index)
            {
                
            }
        }
        public class MinHeap<T> where T : IComparable<T>, IHeapItem
        {
            List<T> A;
            public MinHeap()
            {
                A = new List<T>();
                A.Add(default); // make one-based list
            }

            public int length => A.Count - 1;

            public int L(int i) => i << 1;
            public int R(int i) => (i << 1) | 1;
            public int P(int i) => i >> 1;
            public void Heapify(int i)
            {
                int j = i;
                if (L(i) <= length && A[L(i)].CompareTo(A[j]) < 0)
                    j = L(i);
                if (R(i) <= length && A[R(i)].CompareTo(A[j]) < 0)  
                    j = R(i);
                if (j != i)
                {
                    Swap(i, j);
                    Heapify(j);
                }
            }
            public T ExtraxtMin()
            {
                int n = length;
                T res = A[1];   
                A[1] = A[n];
                A.RemoveAt(n);
                Heapify(1);
                return res;
            }
            public void Rootify(int i)
            {
                if (i == 1 || A[i].CompareTo(A[P(i)]) >= 0)
                    return;
                Swap(i, P(i));
                Rootify(P(i));
            }
            public void Insert(T k)
            {
                int n = length;
                A[++n] = k;
                Rootify(n);
            }
            private void Swap(int i, int j)
            {
                T a = A[i];
                A[i] = A[j];
                A[j] = a;

                A[i].SetHeapIndex(j);
                A[j].SetHeapIndex(i);
            }
        }
        public interface IHeapItem
        {
            void SetHeapIndex(int index);
        }
    }
}