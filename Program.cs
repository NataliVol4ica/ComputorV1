using System;
using System.Collections.Generic;

//todo: error management

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
                int degree;
                List<double> polynomial = new Polynomial().Parse("2 *x = 2*X ", out degree);
                //"= 2"
                //"x = 2 = 3"
                //"x = 2"
                //"2*x=2*X"
                //"2 + -3 * x ^2 = 0"
                //"2 + ----3 * x ^2 = 0"
                //"2 + 1 * x^2 = 0"
                //"1 * x ^0 + 0 * x^1 + 1 * x^2 = 0"
                //degree of 42x = 42x
                //degree of 0 = 0
                //for (int i = 0; i < 3; i++)
                //    Console.WriteLine(i + " " + polynomial[i]);
                Console.Write("     Reduced form: ");
                Console.Write(new Polynomial().ToString(polynomial));
                Console.WriteLine(" = 0");
                Console.WriteLine("Polynomial degree: " + degree);
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
