using System;
using ComputorV1.Console;

namespace ComputorV1
{
    public class Solution
    {
        public string Expression { get; set; }
        public string ReducedForm { get; set; }
        public int Degree { get; set; }
        public bool IsSolvable => String.IsNullOrEmpty(ErrorMessage);

        public string ErrorMessage
        {
            get
            {
                if (Degree > 2 || Degree < 0)
                    return $"Expected degree: 0..2. Actual degree: {Degree}";
                return "";
            }
        }

        public Solution()
        {
            Degree = -1;
        }

        public void WriteSolution(IConsole console)
        {
            console.WriteLine($"Expression: {Expression}");
            console.WriteLine($"Reduced form: {ReducedForm} = 0");
            console.WriteLine($"Polynomial Degree: {Degree}");
            if (IsSolvable)
            {

            }
            else
            {
                console.WriteLine(ErrorMessage);
            }
        }
    }
}
