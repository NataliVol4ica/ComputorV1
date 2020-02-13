using System;
using System.Collections.Generic;
using ComputorV1.Console;

namespace ComputorV1
{
    public class PolynomialSolver
    {
        private IConsole _console;

        public PolynomialSolver(IConsole console)
        {
            _console = console ?? throw new ArgumentException();
        }

        public int Solve(string[] args)
        {
            try
            {
                string expression = ParseArgs(args);
                SolveExpression(expression);
                return 0;
            }
            catch (Exception ex)
            {
                _console.WriteLine(ex.Message);
                return 1;
            }
        }

        private string ParseArgs(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Please enter a single argument.");
            }

            return args[0];
        }

        private void SolveExpression(string expression)
        {
            _console.WriteLine($"Expression: {expression}");
            List<double> polynomial = Polynomial.Parse(expression);
            Polynomial.ShortenCoef(polynomial);
            _console.WriteLine($"Reduced form: {Polynomial.ToString(polynomial)} = 0");
            _console.WriteLine("Polynomial Degree: " + (polynomial.Count - 1));
            if (polynomial.Count > 3)
                throw new Exception($"Degree has to be 0..2. {polynomial.Count - 1} is not.");
            Polynomial.Solve(polynomial);
        }
    }
}