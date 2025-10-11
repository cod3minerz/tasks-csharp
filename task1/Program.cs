namespace task1
{
    class Program
    {
        static void Main()
        {
            string[] lines = File.ReadAllLines("input.txt");
            
            int N = int.Parse(lines[0]);
            
            double[,] G = new double[N, N];
            double[] x = new double[N];
            
            for (int i = 0; i < N; i++)
            {
                string[] parts = lines[i + 1].Split(' ');
                for (int j = 0; j < N; j++)
                {
                    G[i, j] = double.Parse(parts[j]);
                }
            }
            
            string[] vectorParts = lines[N + 1].Split(' ');
            for (int i = 0; i < N; i++)
            {
                x[i] = double.Parse(vectorParts[i]);
            }
            
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (G[i, j] != G[j, i])
                    {
                        Console.WriteLine("Матрица G не симметрична!");
                        return;
                    }
                }
            }
            
            double[] temp = new double[N];
            for (int i = 0; i < N; i++)
            {
                double sum = 0;
                for (int j = 0; j < N; j++)
                {
                    sum += x[j] * G[j, i];
                }

                temp[i] = sum;
            }

            double result = 0;
            for (int i = 0; i < N; i++)
            {
                result += temp[i] * x[i];
            }

            double length = Math.Sqrt(result);

            Console.WriteLine("Длина вектора = " + length);
        }
    }
}