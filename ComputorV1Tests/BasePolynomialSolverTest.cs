using ComputorV1;
using ComputorV1.Console;
using Moq;

namespace ComputorV1Tests
{
    public class BasePolynomialSolverTest
    {
        protected PolynomialSolver TestedSolver;

        protected readonly Mock<IConsole> ConsoleMock = new Mock<IConsole>();

        public BasePolynomialSolverTest()
        {
            ConsoleMock
                .Setup(c => c.Write(It.IsAny<string>()));
            ConsoleMock
                .Setup(c => c.WriteLine(It.IsAny<string>()));
            ConsoleMock
                .Setup(c => c.Read());
            ConsoleMock
                .Setup(c => c.ReadLine());
        }

        protected void SetupSolver()
        {
            TestedSolver = new PolynomialSolver(ConsoleMock.Object);
        }
    }
}