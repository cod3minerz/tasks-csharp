namespace MyPriority;

using System;
using System.Collections;
using System.Collections.Generic;

public class MyPriorityQueue<T>
{
    private List<T> queue;
    private int size;
    private IComparer<T> comparator;
    
    
    public MyPriorityQueue()
    {
        queue = new List<T>(11);
        comparator = Comparer<T>.Default;
        size = 0;
    }
    
    public MyPriorityQueue(T[] a)
    {
        queue = new List<T>(a);
        comparator = Comparer<T>.Default;
        size = a.Length;
        BuildHeap();
    }
    
    public MyPriorityQueue(int initialCapacity)
    {
        queue = new List<T>(initialCapacity);
        comparator = Comparer<T>.Default;
        size = 0;
    }
    
    public MyPriorityQueue(int initialCapacity, IComparer<T> comp)
    {
        queue = new List<T>(initialCapacity);
        comparator = comp ?? Comparer<T>.Default;
        size = 0;
    }
    
    public MyPriorityQueue(MyPriorityQueue<T> other)
    {
        queue = new List<T>(other.queue);
        comparator = other.comparator;
        size = other.size;
    }
    
    public void Add(T e)
    {
        queue.Add(e);
        size++;
        Up(size - 1);
    }

    
    public void AddAll(T[] a)
    {
        foreach (T e in a)
            Add(e);
    }

    
    public void Clear()
    {
        queue.Clear();
        size = 0;
    }
    
    public bool Contains(object o)
    {
        return queue.Contains((T)o);
    }
    
    public bool ContainsAll(T[] a)
    {
        foreach (T e in a)
            if (!queue.Contains(e)) return false;
        return true;
    }
    
    public bool IsEmpty() => size == 0;
    
    public bool Remove(object o)
    {
        int i = queue.IndexOf((T)o);
        if (i == -1) return false;

        Swap(i, size - 1);
        queue.RemoveAt(size - 1);
        size--;
        Down(i);
        return true;
    }
    
    public void RemoveAll(T[] a)
    {
        foreach (T e in a)
            Remove(e);
    }
    
    public void RetainAll(T[] a)
    {
        HashSet<T> set = new HashSet<T>(a);
        queue.RemoveAll(x => !set.Contains(x));
        size = queue.Count;
        BuildHeap();
    }
    
    public int Size() => size;
    
    public T[] ToArray() => queue.ToArray();
    
    public T Element()
    {
        if (size == 0) throw new InvalidOperationException("Очередь пуста");
        return queue[0];
    }
    
    public bool Offer(T obj)
    {
        try
        {
            Add(obj);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public T Peek() => size == 0 ? default(T) : queue[0];
    
    public T Poll()
    {
        if (size == 0) return default(T);
        T top = queue[0];
        Swap(0, size - 1);
        queue.RemoveAt(size - 1);
        size--;
        Down(0);
        return top;
    }

    private void BuildHeap()
    {
        for (int i = size / 2 - 1; i >= 0; i--)
            Down(i);
    }

    private void Up(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (comparator.Compare(queue[i], queue[parent]) >= 0)
                break;
            Swap(i, parent);
            i = parent;
        }
    }

    private void Down(int i)
    {
        while (true)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int smallest = i;

            if (left < size && comparator.Compare(queue[left], queue[smallest]) < 0)
                smallest = left;
            if (right < size && comparator.Compare(queue[right], queue[smallest]) < 0)
                smallest = right;

            if (smallest == i) break;
            Swap(i, smallest);
            i = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        T tmp = queue[i];
        queue[i] = queue[j];
        queue[j] = tmp;
    }
}