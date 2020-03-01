using System.Collections.Generic;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_SingleRationalSolution : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_SingleRationalSolution_DegreeOne = new List<object[]>
        {
            new object[] {"x  = 2,0", "2"},
            new object[] {"x^20-x^20+   x  = 2,0", "2"},
            new object[] {"x  = 2.0", "2"},
            new object[] {" 2 * x + 2 * x = 0 ", "0"},
            new object[] {"2.0 * x = 2", "1"}
        };

        public static readonly IEnumerable<object[]> ValidTests_SingleRationalSolution_DegreeTwo = new List<object[]>
        {
            new object[] {"x^2 + 4*x + 4 = 0", "-2"},
            new object[] {"2 + ----3 * x ^2 = 0", "-2"}
        };

        [Theory]
        [MemberData(nameof(ValidTests_SingleRationalSolution_DegreeOne))]
        public void Solve_WhenExpressionHasSingleRationalSolutionAndDegreeOne_ShouldWriteProperSolution(string expr,
            string x)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] {expr});

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("Degree: 1"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {x}"))));
        }

        [Theory]
        [MemberData(nameof(ValidTests_SingleRationalSolution_DegreeTwo))]
        public void Solve_WhenExpressionHasSingleRationalSolutionAndDegreeTwo_ShouldWriteProperSolution(string expr,
            string x)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] {expr});

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("D = 0"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains("Degree: 2"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {x}"))));
        }
    }
}