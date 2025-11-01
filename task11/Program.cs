using System;
using System.IO;
using MyVector; // Подключаем пространство имён с новым классом MyVector<T>

namespace Task11
{
    class Program
    {
        static void Main()
        {
            MyVector<string> lines = new MyVector<string>();

            try
            {
                string[] fileLines = File.ReadAllLines("task11.txt");
                lines.AddAll(fileLines);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка чтения файла: " + e.Message);
                return;
            }

            MyVector<string> ipAddresses = new MyVector<string>();

            for (int i = 0; i < lines.Size(); i++)
            {
                string line = lines.Get(i);
                FindIPAddresses(line, ipAddresses);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    for (int i = 0; i < ipAddresses.Size(); i++)
                    {
                        writer.WriteLine(ipAddresses.Get(i));
                    }
                }
                Console.WriteLine("Найдено IP-адресов: " + ipAddresses.Size());
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка записи в файл: " + e.Message);
            }
        }

        static void FindIPAddresses(string line, MyVector<string> result)
        {
            int i = 0;
            while (i < line.Length)
            {
                if (i > 0 && (IsDigit(line[i - 1]) || line[i - 1] == '.'))
                {
                    i++;
                    continue;
                }

                string ip = TryParseIP(line, i);

                if (ip != null)
                {
                    int endPos = i + ip.Length;
                    
                    if (endPos < line.Length && (IsDigit(line[endPos]) || line[endPos] == '.'))
                    {
                        i++;
                        continue; 
                    }

                    result.Add(ip);
                    i = endPos;
                }
                else
                {
                    i++;
                }
            }
        }

        static string TryParseIP(string line, int start)
        {
            int pos = start;
            int partCount = 0;

            for (int part = 0; part < 4; part++)
            {
                if (pos >= line.Length || !IsDigit(line[pos]))
                    return null;

                string num = "";
                while (pos < line.Length && IsDigit(line[pos]))
                {
                    num += line[pos];
                    pos++;
                }

                if (num.Length == 0 || num.Length > 3)
                    return null;

                if (num.Length > 1 && num[0] == '0')
                    return null;

                int value = int.Parse(num);
                if (value > 255)
                    return null;

                partCount++;

                if (part < 3)
                {
                    if (pos >= line.Length || line[pos] != '.')
                        return null;
                    pos++;
                }
            }

            if (partCount == 4)
            {
                return line.Substring(start, pos - start);
            }

            return null;
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}
