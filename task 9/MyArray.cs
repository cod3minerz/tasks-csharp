namespace MyArrayList;

using System;

public class MyArrayList<T>
{
    private T[] elementData; 
    private int size;       
    
    public MyArrayList()
    {
        elementData = new T[10]; 
        size = 0;
    }

    
    public MyArrayList(T[] a)
    {
        elementData = new T[a.Length];
        Array.Copy(a, elementData, a.Length);
        size = a.Length;
    }

    
    public MyArrayList(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentException("Capacity cannot be negative");
        elementData = new T[capacity];
        size = 0;
    }

    
    public void Add(T e)
    {
        EnsureCapacity(size + 1);
        elementData[size] = e;
        size++;
    }
    
    public void AddAll(T[] a)
    {
        EnsureCapacity(size + a.Length);
        for (int i = 0; i < a.Length; i++)
        {
            elementData[size] = a[i];
            size++;
        }
    }
    
    public void Add(int index, T e)
    {
        if (index < 0 || index > size)
            throw new ArgumentOutOfRangeException();
        EnsureCapacity(size + 1);
        for (int i = size; i > index; i--)
        {
            elementData[i] = elementData[i - 1];
        }
        elementData[index] = e;
        size++;
    }
    
    public void AddAll(int index, T[] a)
    {
        if (index < 0 || index > size)
            throw new ArgumentOutOfRangeException();
        EnsureCapacity(size + a.Length);
        for (int i = size - 1; i >= index; i--)
        {
            elementData[i + a.Length] = elementData[i];
        }
        for (int i = 0; i < a.Length; i++)
        {
            elementData[index + i] = a[i];
        }
        size += a.Length;
    }
    
    private void EnsureCapacity(int minCapacity)
    {
        if (minCapacity > elementData.Length)
        {
            int newCapacity = elementData.Length * 3 / 2 + 1;
            if (newCapacity < minCapacity)
                newCapacity = minCapacity;
            T[] newArray = new T[newCapacity];
            Array.Copy(elementData, newArray, size);
            elementData = newArray;
        }
    }
    
    public void Clear()
    {
        elementData = new T[10];
        size = 0;
    }
    
    public bool Contains(T o)
    {
        return IndexOf(o) >= 0;
    }
    
    public bool ContainsAll(T[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (!Contains(a[i]))
                return false;
        }
        return true;
    }
    
    public bool IsEmpty()
    {
        return size == 0;
    }
    
    public bool Remove(T o)
    {
        int index = IndexOf(o);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }
        return false;
    }
    
    public void RemoveAll(T[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            Remove(a[i]);
        }
    }
    
    public void RetainAll(T[] a)
    {
        for (int i = 0; i < size; i++)
        {
            bool keep = false;
            for (int j = 0; j < a.Length; j++)
            {
                if (Equals(elementData[i], a[j]))
                {
                    keep = true;
                    break;
                }
            }
            if (!keep)
            {
                RemoveAt(i);
                i--; 
            }
        }
    }

    
    public int Size()
    {
        return size;
    }
    
    public T Get(int index)
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException();
        return elementData[index];
    }

    
    public void Set(int index, T e)
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException();
        elementData[index] = e;
    }

    
    public int IndexOf(T o)
    {
        for (int i = 0; i < size; i++)
        {
            if (Equals(elementData[i], o))
                return i;
        }
        return -1;
    }

    
    public int LastIndexOf(T o)
    {
        for (int i = size - 1; i >= 0; i--)
        {
            if (Equals(elementData[i], o))
                return i;
        }
        return -1;
    }

    
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= size)
            throw new ArgumentOutOfRangeException();
        T removed = elementData[index];
        for (int i = index; i < size - 1; i++)
        {
            elementData[i] = elementData[i + 1];
        }
        elementData[size - 1] = default(T);
        size--;
        return removed;
    }
    
    public T[] ToArray()
    {
        T[] result = new T[size];
        Array.Copy(elementData, result, size);
        return result;
    }
    
    public T[] ToArray(T[] a)
    {
        if (a == null || a.Length < size)
        {
            a = new T[size];
        }
        Array.Copy(elementData, a, size);
        return a;
    }
    
    public MyArrayList<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
            throw new ArgumentOutOfRangeException();
        int length = toIndex - fromIndex;
        T[] subArray = new T[length];
        Array.Copy(elementData, fromIndex, subArray, 0, length);
        return new MyArrayList<T>(subArray);
    }
}


