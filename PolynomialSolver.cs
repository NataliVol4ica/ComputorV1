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
            var solution = new Solution();
            solution.Expression = expression;
            List<double> polynomial = Polynomial.Parse(expression, solution);
            if (solution.IsValid)
            {
                Polynomial.ShortenCoef(polynomial);
                solution.ReducedForm = Polynomial.ToString(polynomial);
                solution.Degree = polynomial.Count - 1;
                if (solution.IsSolvable)
                    Polynomial.Solve(polynomial, solution);
            }

            solution.WriteSolution(_console);
        }
    }
}