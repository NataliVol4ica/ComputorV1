using System.Collections.Generic;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_AllNumbers : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_AllNumbers = new List<object[]>
        {
            new object[] {"0  = 0"},
            new object[] {"0*x  = 0"},
            new object[] {"5  = 5"},
            new object[] {"2 + 3 + 1  = 6"},
            new object[] {"x^20  = x^20"},
            new object[] {"2*x=2*X"}
        };

        [Theory]
        [MemberData(nameof(ValidTests_AllNumbers))]
        public void Solve_WhenExpressionHasAllSolutions_ShouldWriteProperSolution(string expr)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] {expr});

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("All real numbers"))));
        }
    }
}