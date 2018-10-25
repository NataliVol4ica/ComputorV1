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
                List<double> polynomial = new Polynomial().Parse(2, "x^2");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }
    }
}
