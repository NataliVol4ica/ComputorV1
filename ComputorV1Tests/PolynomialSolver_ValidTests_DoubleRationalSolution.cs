using System.Collections.Generic;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_DoubleRationalSolution : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_DoubleRationalSolution = new List<object[]>
        {
            new object[] {"x^2 - x - 2 = 0", "-1", "2"},
            new object[] {"x^2 - x = 2", "-1", "2"},
            new object[] {"1 * x ^0 + 0 * x^1 - 1 * x^2 = 0", "-1", "1"}
        };

        [Theory]
        [MemberData(nameof(ValidTests_DoubleRationalSolution))]
        public void Solve_WhenExpressionHasDoubleRationalSolution_ShouldWriteProperSolution(string expr,
            string x1, string x2)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] {expr});

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("D > 0"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("Degree: 2"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {x1}"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {x2}"))));
        }
    }
}