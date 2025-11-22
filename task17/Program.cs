using System.Diagnostics;
using ScottPlot;
using task17;

class Program
{
    static readonly Random rand = new Random();

    static void Main()
    {
        int[] sizes = { 100_000, 1_000_000, 10_000_000 };
        int runs = 20;

        MeasureAndPlot(sizes, runs, "Add",        AddOperation);
        MeasureAndPlot(sizes, runs, "Get",        GetOperation);
        MeasureAndPlot(sizes, runs, "Set",        SetOperation);
        MeasureAndPlot(sizes, runs, "AddAtIndex", AddAtIndexOperation);
        MeasureAndPlot(sizes, runs, "Remove",     RemoveOperation);

        Console.WriteLine("Все графики созданы!");
    }
    

    static void MeasureAndPlot(
        int[] sizes,
        int runs,
        string operationName,
        Action<int[], double[], double[], int> operation)
    {
        double[] arrayTimes = new double[sizes.Length];
        double[] linkedTimes = new double[sizes.Length];

        operation(sizes, arrayTimes, linkedTimes, runs);

        var plt = new ScottPlot.Plot();

        double[] xs = sizes.Select(x => (double)x).ToArray();

        var scatterArray = plt.Add.Scatter(xs, arrayTimes);
        scatterArray.Label = "MyArrayList";
        scatterArray.Color = Colors.Blue;

        var scatterLinked = plt.Add.Scatter(xs, linkedTimes);
        scatterLinked.Label = "MyLinkedList";
        scatterLinked.Color = Colors.Red;

        plt.Title($"{operationName} Performance");
        plt.XLabel("Size");
        plt.YLabel("Time (ms)");
        plt.ShowLegend();

        plt.SavePng($"{operationName}_Comparison.png", 1200, 800);
        Console.WriteLine($"График {operationName} сохранён: {operationName}_Comparison.png");
    }
    

    static double TicksToMs(double ticks) =>
        ticks * 1000.0 / Stopwatch.Frequency;

    static MyArrayList<int> BuildArrayList(int size)
    {
        var list = new MyArrayList<int>();
        for (int i = 0; i < size; i++)
            list.Add(i);
        return list;
    }

    static MyLinkedList<int> BuildLinkedList(int size)
    {
        var list = new MyLinkedList<int>();
        for (int i = 0; i < size; i++)
            list.Add(i);
        return list;
    }
    
    static void AddOperation(int[] sizes, double[] arrayTimes, double[] linkedTimes, int runs)
    {
        for (int i = 0; i < sizes.Length; i++)
        {
            int n = sizes[i];
            long arrayTicks = 0;
            long linkedTicks = 0;

            for (int run = 0; run < runs; run++)
            {
                var arrayList = new MyArrayList<int>();
                var linkedList = new MyLinkedList<int>();

                var sw = Stopwatch.StartNew();
                for (int j = 0; j < n; j++)
                    arrayList.Add(j);
                sw.Stop();
                arrayTicks += sw.ElapsedTicks;

                sw.Restart();
                for (int j = 0; j < n; j++)
                    linkedList.AddLast(j);
                sw.Stop();
                linkedTicks += sw.ElapsedTicks;
            }

            arrayTimes[i] = TicksToMs(arrayTicks / (double)runs);
            linkedTimes[i] = TicksToMs(linkedTicks / (double)runs);

            Console.WriteLine($"Add {n}: Array={arrayTimes[i]:F3} ms, Linked={linkedTimes[i]:F3} ms");
        }
    }
    
    static void GetOperation(int[] sizes, double[] arrayTimes, double[] linkedTimes, int runs)
    {
        for (int i = 0; i < sizes.Length; i++)
        {
            int n = sizes[i];

            var arrayList = BuildArrayList(n);
            var linkedList = BuildLinkedList(n);

            long arrayTicks = 0;
            long linkedTicks = 0;

            for (int run = 0; run < runs; run++)
            {
                int index = rand.Next(n);

                var sw = Stopwatch.StartNew();
                _ = arrayList.Get(index);
                sw.Stop();
                arrayTicks += sw.ElapsedTicks;

                sw.Restart();
                _ = linkedList.Get(index);
                sw.Stop();
                linkedTicks += sw.ElapsedTicks;
            }

            arrayTimes[i] = TicksToMs(arrayTicks / (double)runs);
            linkedTimes[i] = TicksToMs(linkedTicks / (double)runs);

            Console.WriteLine($"Get {n}: Array={arrayTimes[i]:F6} ms, Linked={linkedTimes[i]:F6} ms");
        }
    }
    
    static void SetOperation(int[] sizes, double[] arrayTimes, double[] linkedTimes, int runs)
    {
        for (int i = 0; i < sizes.Length; i++)
        {
            int n = sizes[i];

            var arrayList = BuildArrayList(n);
            var linkedList = BuildLinkedList(n);

            long arrayTicks = 0;
            long linkedTicks = 0;

            for (int run = 0; run < runs; run++)
            {
                int index = rand.Next(n);
                int value = run; 

                var sw = Stopwatch.StartNew();
                arrayList.Set(index, value);
                sw.Stop();
                arrayTicks += sw.ElapsedTicks;

                sw.Restart();
                linkedList.Set(index, value);
                sw.Stop();
                linkedTicks += sw.ElapsedTicks;
            }

            arrayTimes[i] = TicksToMs(arrayTicks / (double)runs);
            linkedTimes[i] = TicksToMs(linkedTicks / (double)runs);

            Console.WriteLine($"Set {n}: Array={arrayTimes[i]:F6} ms, Linked={linkedTimes[i]:F6} ms");
        }
    }
    
    static void AddAtIndexOperation(int[] sizes, double[] arrayTimes, double[] linkedTimes, int runs)
    {
        for (int i = 0; i < sizes.Length; i++)
        {
            int n = sizes[i];
            int index = n / 2;
            long arrayTicks = 0;
            long linkedTicks = 0;

            for (int run = 0; run < runs; run++)
            {
                var arrayList = BuildArrayList(n);
                var linkedList = BuildLinkedList(n);

                var sw = Stopwatch.StartNew();
                arrayList.Add(index, 123);
                sw.Stop();
                arrayTicks += sw.ElapsedTicks;

                sw.Restart();
                linkedList.Add(index, 123);
                sw.Stop();
                linkedTicks += sw.ElapsedTicks;
            }

            arrayTimes[i] = TicksToMs(arrayTicks / (double)runs);
            linkedTimes[i] = TicksToMs(linkedTicks / (double)runs);

            Console.WriteLine($"AddAtIndex {n}: Array={arrayTimes[i]:F6} ms, Linked={linkedTimes[i]:F6} ms");
        }
    }
    
    static void RemoveOperation(int[] sizes, double[] arrayTimes, double[] linkedTimes, int runs)
    {
        for (int i = 0; i < sizes.Length; i++)
        {
            int n = sizes[i];
            int index = n / 2;
            long arrayTicks = 0;
            long linkedTicks = 0;

            for (int run = 0; run < runs; run++)
            {
                var arrayList = BuildArrayList(n);
                var linkedList = BuildLinkedList(n);

                var sw = Stopwatch.StartNew();
                arrayList.RemoveAt(index);
                sw.Stop();
                arrayTicks += sw.ElapsedTicks;

                sw.Restart();
                linkedList.RemoveAt(index);
                sw.Stop();
                linkedTicks += sw.ElapsedTicks;
            }

            arrayTimes[i] = TicksToMs(arrayTicks / (double)runs);
            linkedTimes[i] = TicksToMs(linkedTicks / (double)runs);

            Console.WriteLine($"Remove {n}: Array={arrayTimes[i]:F6} ms, Linked={linkedTimes[i]:F6} ms");
        }
    }
}