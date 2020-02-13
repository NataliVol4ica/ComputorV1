using System.Collections.Generic;
using ComputorV1;
using ComputorV1.Console;
using Moq;
using Xunit;

namespace ComputorV1Tests
{
    public class Tests
    {
        #region tests

        public static readonly IEnumerable<object[]> ValidTests = new List<object[]>
        {
            new object[] {"2 * ax abc = 0 ", ""},
            new object[] {"0", ""},
            new object[] {"0 = 0", ""},
            new object[] {"0 = 0 = 0", ""},
            new object[] {"= 0", ""},
            new object[] {"2 = 0", ""},
            new object[] {"x =2,0", ""},
            new object[] {"X =2.0", ""},
            new object[] {"x^20 = 0", ""},
            new object[] {"x^20 = x^20", ""},
            new object[] {"2*x=2*X", ""},
            new object[] {"2 + 1 * x^2 = 0", ""},
            new object[] {"1 * x ^0 + 0 * x^1 + 1 * x^2 = 0", ""},
            new object[] {"1 * x ^0 + 0 * x^1 - 1 * x^2 = 0", ""},
            new object[] {"x^2 - x - 2 = 0", ""},
            new object[] {"x^2 + x - 2 = 0", ""},
            new object[] {"x^2 + 4*x + 4 = 0", ""},
            new object[] {"x^2 - 6*x + 34=0", ""},
            new object[] {"2 + ----3 * x ^2 = 0", ""},
            new object[] {" 2 * x + 2 * x = 0 ", ""},
        };

        private List<string> _writeMessages = new List<string>();
        private List<string> _writeLineMessages = new List<string>();

        #endregion tests

        private PolynomialSolver _testedSolver;

        private Mock<IConsole> _consoleMock = new Mock<IConsole>();

        public Tests()
        {
            _consoleMock
                .Setup(c=>c.Write(It.IsAny<string>()))
                .Callback<string>(s=> _writeMessages.Add(s));
            _consoleMock
                .Setup(c=>c.WriteLine(It.IsAny<string>()))
                .Callback<string>(s => _writeLineMessages.Add(s));
            _consoleMock
                .Setup(c=>c.Read());
            _consoleMock
                .Setup(c=>c.ReadLine());
            _testedSolver = new PolynomialSolver(_consoleMock.Object);
        }

        [Theory]
        [MemberData(nameof(ValidTests))]
        public void Test1(string expression, string expected)
        {

        }
    }
}



