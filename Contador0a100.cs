using System;

class Program
{
    static void Main(string[] args)
    {
        Count(0);
    }

    static void Count(int num)
    {
        if (num <= 100)
        {
            Console.WriteLine(num);
            Count(num + 1);
        }
    }
}
