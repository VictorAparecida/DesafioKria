using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numeros = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

        foreach (int num in numeros)
        {
            switch (num)
            {
                case 9:
                    Console.Write("09");
                    break;
                default:
                    Console.Write(num);
                    break;
            }
        }
        Console.WriteLine();
    }
}
