using MyVector;

public class MyStack<T> : MyVector<T>
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

        int topIndex = Size() - 1;
        T value = Get(topIndex);
        Remove(topIndex);
        return value;
    }
        
    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст");

        return Get(Size() - 1);
    }
        
    public bool Empty()
    {
        return IsEmpty();
    }
        
    public int Search(T item)
    {
        int index = LastIndexOf(item);
        if (index == -1) return -1;
            
        return Size() - index;
    }
}