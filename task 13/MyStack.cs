using MyPriorityQueue;
using System;

namespace MyStack
{
    
}
public class MyStack<T> : MyPriorityQueue<T>
{
    public MyStack() : base() { }

    public MyStack(int capacity) : base(capacity) { }

    public MyStack(T[] array) : base(array) { }
    
    public void Push(T item)
    {
        Add(item);
    }
    
    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст");

        T[] arr = ToArray();
        T value = arr[Size() - 1];

        Remove(value);
        return value;
    }
    
    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст");

        T[] arr = ToArray();
        return arr[Size() - 1];
    }
    
    public bool Empty()
    {
        return IsEmpty();
    }
    
    public int Search(T item)
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст");

        T[] arr = ToArray();
        
        for (int i = arr.Length - 1, depth = 1; i >= 0; i--, depth++)
        {
            if (Equals(arr[i], item))
                return depth; 
        }

        return -1;
    }
}
