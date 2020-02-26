namespace Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            var values = new List<int> { 1, 2, 3, 4, 5 };

            var squares = values.Select(Square);

            foreach (var s in squares)
            {
                Console.WriteLine($"R: {s}");
            }

            int Square(int value) => value * value;
        }
    }
}