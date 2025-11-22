using System;

public class MyLinkedList<T>
{
    private class Node
    {
        public T Item;
        public Node Prev;
        public Node Next;

        public Node(T item, Node prev, Node next)
        {
            Item = item;
            Prev = prev;
            Next = next;
        }
    }

    private Node first;
    private Node last;
    private int size;
    
    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }
    
    public MyLinkedList(T[] a)
    {
        first = null;
        last = null;
        size = 0;

        if (a != null)
            AddAll(a);
    }
    
    private void CheckIndex(int index)
    {
        if (index < 0 || index >= size)
            throw new IndexOutOfRangeException("Index: " + index + ", Size: " + size);
    }
    
    private void CheckIndexForAdd(int index)
    {
        if (index < 0 || index > size)
            throw new IndexOutOfRangeException("Index: " + index + ", Size: " + size);
    }
    
    private Node GetNode(int index)
    {
        CheckIndex(index);
        
        if (index < size / 2)
        {
            Node x = first;
            for (int i = 0; i < index; i++)
                x = x.Next;
            return x;
        }
        else
        {
            Node x = last;
            for (int i = size - 1; i > index; i--)
                x = x.Prev;
            return x;
        }
    }
    
    private void LinkFirst(T e)
    {
        Node f = first;
        Node newNode = new Node(e, null, f);
        first = newNode;

        if (f == null)
            last = newNode;
        else
            f.Prev = newNode;

        size++;
    }

    private void LinkLast(T e)
    {
        Node l = last;
        Node newNode = new Node(e, l, null);
        last = newNode;

        if (l == null)
            first = newNode;
        else
            l.Next = newNode;

        size++;
    }
    
    private T Unlink(Node x)
         {
             T element = x.Item;
             Node next = x.Next;
             Node prev = x.Prev;
     
             if (prev == null)
                 first = next;
             else
                 prev.Next = next;
     
             if (next == null)
                 last = prev;
             else
                 next.Prev = prev;
     
             x.Item = default(T);
             x.Next = null;
             x.Prev = null;
     
             size--;
             return element;
         }
    
    public bool Add(T e)
    {
        LinkLast(e);
        return true;
    }
    
    public bool AddAll(T[] a)
    {
        if (a == null || a.Length == 0)
            return false;

        for (int i = 0; i < a.Length; i++)
            LinkLast(a[i]);

        return true;
    }
    
    public void Clear()
    {
        Node x = first;
        while (x != null)
        {
            Node next = x.Next;
            x.Item = default(T);
            x.Prev = null;
            x.Next = null;
            x = next;
        }

        first = null;
        last = null;
        size = 0;
    }
    
    public bool Contains(object o)
    {
        return IndexOf(o) != -1;
    }
    
    public bool ContainsAll(T[] a)
    {
        if (a == null) return false;
        for (int i = 0; i < a.Length; i++)
            if (!Contains(a[i])) return false;
        return true;
    }
    
    public bool IsEmpty()
    {
        return size == 0;
    }
    
    public bool Remove(object o)
    {
        Node x = first;
        while (x != null)
        {
            if ((o == null && x.Item == null) || (o != null && o.Equals(x.Item)))
            {
                Unlink(x);
                return true;
            }
            x = x.Next;
        }
        return false;
    }
    
    public bool RemoveAll(T[] a)
    {
        if (a == null) return false;
        bool changed = false;

        for (int i = 0; i < a.Length; i++)
        {
            while (Remove(a[i]))
                changed = true;
        }

        return changed;
    }
    
    public bool RetainAll(T[] a)
    {
        if (a == null) return false;

        bool changed = false;
        Node x = first;

        while (x != null)
        {
            Node next = x.Next;

            bool found = false;
            for (int i = 0; i < a.Length; i++)
            {
                if ((a[i] == null && x.Item == null) ||
                    (a[i] != null && a[i].Equals(x.Item)))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Unlink(x);
                changed = true;
            }

            x = next;
        }

        return changed;
    }
    
    public int Size()
    {
        return size;
    }
    
    public object[] ToArray0()
    {
        object[] r = new object[size];
        int i = 0;
        Node x = first;

        while (x != null)
        {
            r[i++] = x.Item;
            x = x.Next;
        }

        return r;
    }
    
    public T[] ToArray(T[] a)
    {
        if (a == null)
        {
            T[] newArr = new T[size];
            Node x = first;
            int i = 0;
            while (x != null)
            {
                newArr[i++] = x.Item;
                x = x.Next;
            }
            return newArr;
        }

        if (a.Length < size)
        {
            T[] newArr = new T[size];
            Node x = first;
            int i = 0;
            while (x != null)
            {
                newArr[i++] = x.Item;
                x = x.Next;
            }
            return newArr;
        }

        Node y = first;
        int j = 0;

        while (y != null)
        {
            a[j++] = y.Item;
            y = y.Next;
        }

        if (a.Length > size)
            a[size] = default(T);

        return a;
    }

    
    public void Add(int index, T e)
    {
        CheckIndexForAdd(index);

        if (index == size)
        {
            LinkLast(e);
            return;
        }

        if (index == 0)
        {
            LinkFirst(e);
            return;
        }

        Node succ = GetNode(index);
        Node pred = succ.Prev;

        Node newNode = new Node(e, pred, succ);
        pred.Next = newNode;
        succ.Prev = newNode;
        size++;
    }
    
    public bool AddAll(int index, T[] a)
    {
        CheckIndexForAdd(index);

        if (a == null || a.Length == 0)
            return false;

        if (index == size)
        {
            AddAll(a);
            return true;
        }

        Node succ = GetNode(index);
        Node pred = succ.Prev;

        for (int i = 0; i < a.Length; i++)
        {
            Node newNode = new Node(a[i], pred, null);

            if (pred == null)
                first = newNode;
            else
                pred.Next = newNode;

            pred = newNode;
            size++;
        }

        pred.Next = succ;
        succ.Prev = pred;

        return true;
    }
    
    public T Get(int index)
    {
        return GetNode(index).Item;
    }
    
    public int IndexOf(object o)
    {
        int i = 0;
        Node x = first;

        while (x != null)
        {
            if ((o == null && x.Item == null) || (o != null && o.Equals(x.Item)))
                return i;

            x = x.Next;
            i++;
        }

        return -1;
    }
    
    public int LastIndexOf(object o)
    {
        int i = size - 1;
        Node x = last;

        while (x != null)
        {
            if ((o == null && x.Item == null) || (o != null && o.Equals(x.Item)))
                return i;

            x = x.Prev;
            i--;
        }

        return -1;
    }
    
    public T RemoveAt(int index)
    {
        CheckIndex(index);
        return Unlink(GetNode(index));
    }
    
    public T Set(int index, T e)
    {
        Node x = GetNode(index);
        T old = x.Item;
        x.Item = e;
        return old;
    }
    
    public MyLinkedList<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
            throw new IndexOutOfRangeException();

        MyLinkedList<T> result = new MyLinkedList<T>();

        Node x = (fromIndex == 0) ? first : GetNode(fromIndex);

        for (int i = fromIndex; i < toIndex; i++)
        {
            result.Add(x.Item);
            x = x.Next;
        }

        return result;
    }
    
    public T Element()
    {
        if (first == null)
            throw new InvalidOperationException("List is empty");

        return first.Item;
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
    
    public T Peek()
    {
        if (first == null) return default(T);
        return first.Item;
    }
    
    public T Poll()
    {
        if (first == null) return default(T);
        return Unlink(first);
    }
    
    public void AddFirst(T obj)
    {
        LinkFirst(obj);
    }
    
    public void AddLast(T obj)
    {
        LinkLast(obj);
    }
    
    public T GetFirst()
    {
        if (first == null)
            throw new InvalidOperationException("List empty");
        return first.Item;
    }
    
    public T GetLast()
    {
        if (last == null)
            throw new InvalidOperationException("List empty");
        return last.Item;
    }
    
    public bool OfferFirst(T obj)
    {
        try
        {
            AddFirst(obj);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool OfferLast(T obj)
    {
        try
        {
            AddLast(obj);
            return true;
        }
        catch
        {
            return false;
        }
    }

    
    public T Pop()
    {
        return RemoveFirst();
    }

    
    public void Push(T obj)
    {
        AddFirst(obj);
    }

    
    public T PeekFirst()
    {
        if (first == null) return default(T);
        return first.Item;
    }

    
    public T PeekLast()
    {
        if (last == null) return default(T);
        return last.Item;
    }

    
    public T PollFirst()
    {
        if (first == null) return default(T);
        return Unlink(first);
    }
    
    public T PollLast()
    {
        if (last == null) return default(T);
        return Unlink(last);
    }
    
    public T RemoveLast()
    {
        if (last == null)
            throw new InvalidOperationException("List empty");
        return Unlink(last);
    }
    
    public T RemoveFirst()
    {
        if (first == null)
            throw new InvalidOperationException("List empty");
        return Unlink(first);
    }
    
    public bool RemoveLastOccurrence(object obj)
    {
        Node x = last;
        while (x != null)
        {
            if ((obj == null && x.Item == null) || (obj != null && obj.Equals(x.Item)))
            {
                Unlink(x);
                return true;
            }
            x = x.Prev;
        }
        return false;
    }
    
    public bool RemoveFirstOccurrence(object obj)
    {
        return Remove(obj); 
    }

}

public class Program
{
    public static void Main(string[] args)
    {
        var list = new MyLinkedList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Console.WriteLine("Содержимое:");
        Print(list);

        list.Remove(2);

        Console.WriteLine("После удаления 2:");
        Print(list);

        Console.WriteLine("Первый элемент: " + list.GetFirst());
        Console.WriteLine("Последний элемент: " + list.GetLast());
    }

    static void Print<T>(MyLinkedList<T> list)
    {
        var arr = list.ToArray0();
        Console.WriteLine("[" + string.Join(", ", arr) + "]");
    }
}
