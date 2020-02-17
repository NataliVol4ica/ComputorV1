using System.Collections.Generic;
using System.Linq;
using ComputorV1;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_ValidTests_SingleRationalSolution : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests_SingleRationalSolution_DegreeOne = new List<object[]>
        {
            new object[] {"x  = 2,0", new Solution {Answers = new List<string> {"2"}}},
            new object[] {"x  = 2.0", new Solution {Answers = new List<string> {"2"}}},
            new object[] {" 2 * x + 2 * x = 0 ", new Solution {Answers = new List<string> {"-2"}}},
            new object[] {"1 * x ^0 + 0 * x^1 - 1 * x^2 = 0", new Solution {Answers = new List<string> {"-2"}}}
        };

        public static readonly IEnumerable<object[]> ValidTests_SingleRationalSolution_DegreeTwo = new List<object[]>
        {
            new object[] {"x^2 + 4*x + 4 = 0", new Solution {Answers = new List<string> {"-2"}}},
            new object[] {"x^2 - 6*x + 34=0", new Solution {Answers = new List<string> {"-2"}}},
            new object[] {"2 + ----3 * x ^2 = 0", new Solution {Answers = new List<string> {"-2"}}}
        };

        [TheoryAttribute]
        [MemberData(nameof(ValidTests_SingleRationalSolution_DegreeOne))]
        public void Solve_WhenExpressionHasSingleRationalSolutionAndDegreeOne_ShouldWriteProperSolution(string expr,
            Solution solution)
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
                    s => s.Contains($" = {solution.Answers.First()}"))));
        }

        [Theory]
        [MemberData(nameof(ValidTests_SingleRationalSolution_DegreeTwo))]
        public void Solve_WhenExpressionHasSingleRationalSolutionAndDegreeTwo_ShouldWriteProperSolution(string expr,
            Solution solution)
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
                    s => s.Contains($" = {solution.Answers.First()}"))));
        }
    }
}