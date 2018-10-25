using System;
using System.Collections.Generic;

//todo: 0 = 0
//todo: 42 = 0
//todo: 2 + -3 * x ^2
//todo: adequate polinom writer
namespace ComputorV1
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                List<double> polynomial = new Polynomial().Parse("2 + 1 * x^2 = 0");
                //1 * x ^0 + 0 * x^1 + 1 * x^2 = 0
                Console.Write("Reduced form: ");
                new Polynomial().Write(polynomial);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            return 0;
        }
    }
}
