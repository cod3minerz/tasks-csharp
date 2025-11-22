using System;

public class MyArrayDeque<T>
{
    private T[] elements; 
    private int head;     
    private int tail;    
    private int size;    

    private const int DEFAULT_CAPACITY = 16;
    
    public MyArrayDeque()
    {
        elements = new T[DEFAULT_CAPACITY];
        head = 0;
        tail = 0;
        size = 0;
    }
    
    public MyArrayDeque(T[] a)
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));

        elements = new T[Math.Max(DEFAULT_CAPACITY, a.Length)];
        Array.Copy(a, elements, a.Length);
        size = a.Length;
        head = 0;
        tail = size % elements.Length;
    }
    
    public MyArrayDeque(int numElements)
    {
        if (numElements <= 0)
            throw new ArgumentException("Вместимость должна быть положительной");
        elements = new T[numElements];
        head = 0;
        tail = 0;
        size = 0;
    }
    
    private void Grow()
    {
        int newCapacity = elements.Length * 2;
        T[] newArr = new T[newCapacity];

        for (int i = 0; i < size; i++)
            newArr[i] = elements[(head + i) % elements.Length];

        elements = newArr;
        head = 0;
        tail = size;
    }
    
    public void Add(T e)
    {
        if (size == elements.Length)
            Grow();

        elements[tail] = e;
        tail = (tail + 1) % elements.Length;
        size++;
    }
    
    public void AddAll(T[] a)
    {
        if (a == null)
            throw new ArgumentNullException(nameof(a));

        foreach (T item in a)
            Add(item);
    }
    
    public void Clear()
    {
        elements = new T[DEFAULT_CAPACITY];
        size = 0;
        head = 0;
        tail = 0;
    }
    
    public bool Contains(object o)
    {
        for (int i = 0; i < size; i++)
        {
            int idx = (head + i) % elements.Length;
            if (Equals(elements[idx], o))
                return true;
        }
        return false;
    }
    
    public bool ContainsAll(T[] a)
    {
        foreach (T item in a)
        {
            if (!Contains(item))
                return false;
        }
        return true;
    }
    
    public bool IsEmpty()
    {
        return size == 0;
    }
    
    public bool Remove(object o)
    {
        for (int i = 0; i < size; i++)
        {
            int idx = (head + i) % elements.Length;
            if (Equals(elements[idx], o))
            {
                RemoveAt(idx);
                return true;
            }
        }
        return false;
    }

    private void RemoveAt(int index)
    {
        if (index < 0 || index >= elements.Length)
            throw new ArgumentOutOfRangeException();

        for (int i = index; i != tail; i = (i + 1) % elements.Length)
        {
            int next = (i + 1) % elements.Length;
            elements[i] = elements[next];
        }

        tail = (tail - 1 + elements.Length) % elements.Length;
        elements[tail] = default(T);
        size--;
    }
    
    public void RemoveAll(T[] a)
    {
        foreach (T item in a)
            Remove(item);
    }
    
    public void RetainAll(T[] a)
    {
        for (int i = 0; i < size; i++)
        {
            int idx = (head + i) % elements.Length;
            if (Array.IndexOf(a, elements[idx]) == -1)
                Remove(elements[idx]);
        }
    }
    
    public int Size()
    {
        return size;
    }
    
    public object[] ToArray()
    {
        object[] arr = new object[size];
        for (int i = 0; i < size; i++)
            arr[i] = elements[(head + i) % elements.Length];
        return arr;
    }
    
    public T[] ToArray(T[] a)
    {
        if (a == null || a.Length < size)
            a = new T[size];

        for (int i = 0; i < size; i++)
            a[i] = elements[(head + i) % elements.Length];

        return a;
    }
    
    public T Element()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");
        return elements[head];
    }
    
    public bool Offer(T obj)
    {
        Add(obj);
        return true;
    }
    
    public T Peek()
    {
        if (IsEmpty())
            return default(T);
        return elements[head];
    }
    
    public T Poll()
    {
        if (IsEmpty())
            return default(T);

        T value = elements[head];
        elements[head] = default(T);
        head = (head + 1) % elements.Length;
        size--;
        return value;
    }
    
    public void AddFirst(T obj)
    {
        if (size == elements.Length)
            Grow();

        head = (head - 1 + elements.Length) % elements.Length;
        elements[head] = obj;
        size++;
    }
    
    public void AddLast(T obj)
    {
        Add(obj);
    }
    
    public T GetFirst()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");
        return elements[head];
    }
    
    public T GetLast()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");
        return elements[(tail - 1 + elements.Length) % elements.Length];
    }
    
    public bool OfferFirst(T obj)
    {
        AddFirst(obj);
        return true;
    }
    
    public bool OfferLast(T obj)
    {
        AddLast(obj);
        return true;
    }
    
    public T Pop()
    {
        return PollFirst();
    }
    
    public void Push(T obj)
    {
        AddFirst(obj);
    }
    
    public T PeekFirst()
    {
        return Peek();
    }
    
    public T PeekLast()
    {
        if (IsEmpty())
            return default(T);
        return elements[(tail - 1 + elements.Length) % elements.Length];
    }
    
    public T PollFirst()
    {
        return Poll();
    }
    
    public T PollLast()
    {
        if (IsEmpty())
            return default(T);

        tail = (tail - 1 + elements.Length) % elements.Length;
        T value = elements[tail];
        elements[tail] = default(T);
        size--;
        return value;
    }
    
    public T RemoveLast()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");
        return PollLast();
    }
    
    public T RemoveFirst()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста");
        return PollFirst();
    }
    
    public bool RemoveLastOccurrence(object obj)
    {
        for (int i = size - 1; i >= 0; i--)
        {
            int idx = (head + i) % elements.Length;
            if (Equals(elements[idx], obj))
            {
                RemoveAt(idx);
                return true;
            }
        }
        return false;
    }
    
    public bool RemoveFirstOccurrence(object obj)
    {
        return Remove(obj);
    }
}

class Program
{
    static void Main()
    {
        MyArrayDeque<int> deque = new MyArrayDeque<int>();
        
        
        deque.AddLast(10);
        deque.AddLast(20);
        deque.AddFirst(5);

        Console.WriteLine("Первый элемент: " + deque.GetFirst()); 
        Console.WriteLine("Последний элемент: " + deque.GetLast()); 
        Console.WriteLine("Размер: " + deque.Size()); 
        
        Console.WriteLine("Удалён первый: " + deque.RemoveFirst()); 
        Console.WriteLine("Удалён последний: " + deque.RemoveLast()); 

        Console.WriteLine("Остался элемент: " + deque.GetFirst()); 
        Console.WriteLine("Размер после удалений: " + deque.Size()); 
        
        deque.Clear();
        Console.WriteLine("Пуста ли очередь: " + deque.IsEmpty()); 
    }
}
