using System.Collections.Generic;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_IrrationalSolution : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_IrrationalSolution = new List<object[]>
        {
            new object[] {"x^2 = -1", "+i", "-i"},
            new object[] {"4 + 1 * x^2 = 0", "+ 2i", "- 2i"},
            new object[] {"x^2 - 6*x + 34=0","3 + 5i", "3 - 5i" },
            new object[] {"1 * x ^0 + 0 * x^1 + 1 * x^2 = 0", "+i", "-i"}
        };

        [Theory]
        [MemberData(nameof(ValidTests_IrrationalSolution))]
        public void Solve_WhenExpressionHasDoubleRationalSolution_ShouldWriteProperSolution(string expr,
            string x1, string x2)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] { expr });

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("D < 0"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($"Degree: 2"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {x1}"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {x2}"))));
        }
    }
}