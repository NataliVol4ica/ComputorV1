using System.Collections.Generic;

namespace ComputorV1
{
    class Program
    {
        static int Main(string[] args)
        {
            List<int> polynomial = new Polynomial().Parse(2, "x^2");
            return 0;
        }
    }
}
