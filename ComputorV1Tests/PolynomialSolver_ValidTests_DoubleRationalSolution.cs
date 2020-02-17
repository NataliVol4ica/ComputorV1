using System.Collections.Generic;
using ComputorV1;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_DoubleRationalSolution : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_DoubleRationalSolution = new List<object[]>
        {
            new object[] {"x^2 - x - 2 = 0", new Solution {Answers = new List<string> {"-1", "2"}}},
            new object[] {"x^2 - x = 2", new Solution {Answers = new List<string> {"-1", "2"}}}
        };

        [Theory]
        [MemberData(nameof(ValidTests_DoubleRationalSolution))]
        public void Solve_WhenExpressionHasDoubleRationalSolution_ShouldWriteProperSolution(string expr,
            Solution solution)
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
                    s => s.Contains($"Degree: {solution.Degree}"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {solution.Answers[0]}"))));
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains($" = {solution.Answers[1]}"))));
        }
    }
}