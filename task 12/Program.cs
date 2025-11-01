using System;
using MyVector;

namespace MyStack
{
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
    
    class Program
    {
        static void Main()
        {
            MyStack<int> stack = new MyStack<int>();

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            Console.WriteLine("Верх стека: " + stack.Peek()); 
            Console.WriteLine("Извлекли: " + stack.Pop());     
            Console.WriteLine("Теперь верх: " + stack.Peek()); 
            Console.WriteLine("Пуст ли стек: " + stack.Empty());

            stack.Push(40);
            stack.Push(50);
            Console.WriteLine("Стек: " + stack); 

            Console.WriteLine("Глубина 10: " + stack.Search(10)); 
            Console.WriteLine("Глубина 50: " + stack.Search(50));
            Console.WriteLine("Глубина 99: " + stack.Search(99)); 
        }
    }
}
