using ComputorV1.Console;

//todo: test error management
//todo: program should take args. first expr: steps: etc foreach str

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