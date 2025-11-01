using System;
using System.Collections.Generic;

class Heap
{
    List<int> data = new List<int>();

    public Heap() { }
    public Heap(int[] arr)
    {
        data.AddRange(arr);
        for (int i = data.Count / 2 - 1; i >= 0; i--) Down(i);
    }

    public int Size() => data.Count;
    public bool Empty() => data.Count == 0;
    public int Peek() => data[0];

    public void Add(int x)
    {
        data.Add(x);
        Up(data.Count - 1);
    }

    public int Extract()
    {
        int root = data[0];
        data[0] = data[^1];
        data.RemoveAt(data.Count - 1);
        if (data.Count > 0) Down(0);
        return root;
    }

    public void IncreaseKey(int i, int newVal)
    {
        if (newVal < data[i]) return;
        data[i] = newVal;
        Up(i);
    }

    public void Merge(Heap other)
    {
        data.AddRange(other.data);
        for (int i = data.Count / 2 - 1; i >= 0; i--) Down(i);
    }

    void Up(int i)
    {
        while (i > 0)
        {
            int p = (i - 1) / 2;
            if (data[p] >= data[i]) break;
            (data[p], data[i]) = (data[i], data[p]);
            i = p;
        }
    }

    void Down(int i)
    {
        while (true)
        {
            int l = 2 * i + 1, r = 2 * i + 2, largest = i;
            if (l < data.Count && data[l] > data[largest]) largest = l;
            if (r < data.Count && data[r] > data[largest]) largest = r;
            if (largest == i) break;
            (data[i], data[largest]) = (data[largest], data[i]);
            i = largest;
        }
    }
}

class Program
{
    static void Main()
    {
        int[] arr = { 5, 3, 8, 1, 6, 2 };
        Heap h = new Heap(arr);

        Console.WriteLine("Max: " + h.Peek());
        h.Add(10);
        Console.WriteLine("Extract: " + h.Extract());
        h.IncreaseKey(2, 15);
        Console.WriteLine("After increase: " + h.Peek());

        Heap h2 = new Heap(new int[] { 4, 9, 7 });
        h.Merge(h2);
        Console.WriteLine("After merge extract: " + h.Extract());
    }
}