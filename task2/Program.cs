struct Complex
{
    public double Re;
    public double Im; 
    
    
    public Complex(double re, double im)
    {
        Re = re;
        Im = im;
    }
    
    public Complex Add(Complex other)
    {
        return new Complex(Re + other.Re, Im + other.Im);
    }
    
    public Complex Sub(Complex other)
    {
        return new Complex(Re - other.Re, Im - other.Im);
    }
    
    public Complex Mul(Complex other)
    {
        return new Complex(Re * other.Re - Im * other.Im,
                           Re * other.Im + Im * other.Re);
    }
    
    public Complex Div(Complex other)
    {
        double denom = other.Re * other.Re + other.Im * other.Im;
        return new Complex((Re * other.Re + Im * other.Im) / denom,
                           (Im * other.Re - Re * other.Im) / denom);
    }
    
    public double Abs()
    {
        return Math.Sqrt(Re * Re + Im * Im);
    }
    
    public double Arg()
    {
        return Math.Atan2(Im, Re);
    }
    
    public override string ToString()
    {
        return $"{Re} {(Im >= 0 ? "+" : "-")} {Math.Abs(Im)}i";
    }
}

class Program
{
    static void Main()
    {
        Complex num = new Complex(0, 0); 
        while (true)
        {
            Console.WriteLine("\nТекущее число: " + num);
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("A - Ввести новое число");
            Console.WriteLine("S - Сложить");
            Console.WriteLine("D - Вычесть");
            Console.WriteLine("M - Умножить");
            Console.WriteLine("V - Разделить");
            Console.WriteLine("R - Найти модуль");
            Console.WriteLine("G - Найти аргумент");
            Console.WriteLine("Q - Выход");
            Console.Write("Ваш выбор: ");

            char choice = Char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            switch (choice)
            {
                case 'A':
                    Console.Write("Введите Re: ");
                    double re = double.Parse(Console.ReadLine());
                    Console.Write("Введите Im: ");
                    double im = double.Parse(Console.ReadLine());
                    num = new Complex(re, im);
                    break;

                case 'S':
                    Complex add = InputComplex();
                    num = num.Add(add);
                    break;

                case 'D':
                    Complex sub = InputComplex();
                    num = num.Sub(sub);
                    break;

                case 'M':
                    Complex mul = InputComplex();
                    num = num.Mul(mul);
                    break;

                case 'V':
                    Complex div = InputComplex();
                    num = num.Div(div);
                    break;

                case 'R':
                    Console.WriteLine("Модуль = " + num.Abs());
                    break;

                case 'G':
                    Console.WriteLine("Аргумент (в радианах) = " + num.Arg());
                    break;

                case 'Q':
                    Console.WriteLine("Выход...");
                    return;

                default:
                    Console.WriteLine("Неизвестная команда!");
                    break;
            }
        }
    }

    static Complex InputComplex()
    {
        Console.Write("Введите Re: ");
        double re = double.Parse(Console.ReadLine());
        Console.Write("Введите Im: ");
        double im = double.Parse(Console.ReadLine());
        return new Complex(re, im);
    }
}