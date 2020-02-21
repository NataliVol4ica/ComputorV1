using ComputorV1.Console;

namespace ComputorV1
{
    class Program
    {
        static int Main(string[] args)
        {
            var solver = new PolynomialSolver(new MyConsole());
            return solver.Solve(args);
        }
    }
}