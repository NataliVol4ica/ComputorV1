using System;
using System.Collections.Generic;

//todo: 0 = 0
//todo: 42 = 0
namespace ComputorV1
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                List<double> polynomial = new Polynomial().Parse("x= 2=3");
                // "= 2"
                // "x = 2 = 3"
                // "x = 2"
                //"2 + -3 * x ^2 = 0"
                //"2 + ----3 * x ^2 = 0"
                //"2 + 1 * x^2 = 0"
                //"1 * x ^0 + 0 * x^1 + 1 * x^2 = 0"
                //for (int i = 0; i < 3; i++)
                //    Console.WriteLine(i + " " + polynomial[i]);
                Console.Write("Reduced form: ");
                new Polynomial().Write(polynomial);
                Console.WriteLine(" = 0");
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
