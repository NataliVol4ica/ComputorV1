using System.Collections.Generic;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class PolynomialSolver_InvalidTests : BasePolynomialSolverTest
    {
        public static readonly IEnumerable<object[]> InvalidTests = new List<object[]>
        {
            new object[] {"0", "Expression is missing '='"},
            new object[] {"0 = 0 = 0", "Expression cannot have more than one equation"},
            new object[] {"= 0", "Expression is missing it's left part"},
            new object[] {"x^20  = 0", "Expected degree: 0..2. Actual degree: 20" }
        };

        [Theory]
        [MemberData(nameof(InvalidTests))]
        public void Solve_WhenCalledWithInvalidExpression_ShouldWriteError(string expression, string errorMessage)
        {
            //Arrange
            SetupSolver();

            //Act
            TestedSolver.Solve(new[] { expression });

            //Assert
            ConsoleMock
                .Verify(c => c.WriteLine(It.Is<string>(
                    s => s.Contains(errorMessage))));
        }
    }
}