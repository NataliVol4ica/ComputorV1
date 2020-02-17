using System.Collections.Generic;
using ComputorV1;
using Xunit;

// ReSharper disable All

namespace ComputorV1Tests
{
    public class PolynomialSolverTests : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> ValidTests = new List<object[]>
        {
            //new object[] {"2 * ax abc = 0 ", new Solution{ValidationError = }},
            new object[] {"0", new Solution {Answers = new List<string>()}},
            new object[] {"0 = 0", new Solution {Answers = new List<string>()}},
            //new object[] {"0 = 0 = 0", ""},
            //new object[] {"= 0", ""},
            new object[] {"2 = 0", ""},

            new object[] {"x^20  = 0", new Solution {Degree = 20}}
        };

        [Theory]
        [MemberData(nameof(ValidTests))]
        public void Solve_WhenCalledWithValidExpression_ShouldWriteProperSolution(string expression, Solution expected)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] {expression});

            //Assert
        }
    }
}