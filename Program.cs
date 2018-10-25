using System;
using System.Collections.Generic;

namespace ComputorV1
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                List<double> polynomial = new Polynomial().Parse("x^3 = 0");
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
