using System.Collections.Generic;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_NoSolution : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_NoSolution = new List<object[]>
        {
            new object[] {"0  = 1"},
            new object[] {"0*x  = 1"},
            new object[] {"2 + 3 + 1  = 7"}
        };

        [Theory]
        [MemberData(nameof(ValidTests_NoSolution))]
        public void Solve_WhenExpressionHasNoSolutions_ShouldWriteProperSolution(string expr)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] {expr});

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("None"))));
        }
    }
}