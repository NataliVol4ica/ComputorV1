using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ComputorV1
{
    public class Polynomial
    {
        #region Enums and Structs
        protected enum TokenType
        {
            Number,
            Operator,
            Pow,
            Var
        }
        struct PolyToken
        {
            public PolyToken(string str, TokenType tokenType)
            {
                this.str = str;
                this.tokenType = tokenType;
            }
            public TokenType tokenType;
            public string str;
        }
        #endregion

        #region Variables
        private static readonly Regex tokenRegEx =
           new Regex(@"\G\s*(\d+(\.\d+)?|[a-z]|\+|-|\*|\^|=)\s*", RegexOptions.Compiled);
        #endregion

        #region Public
        public List<int> Parse(int degree, string expression)
        {
            List<int> coefficients = new List<int>();
            Queue<string> stringTokens = Tokenize(expression);

            return coefficients;
        }
        #endregion
        #region Protected
        protected virtual Queue<string> Tokenize(string expression)
        {
            Queue<string> stringTokens = new Queue<string>();
            int lastMatchPos = 0;
            int lastMatchLen = 0;
            Match match = tokenRegEx.Match(expression);
            while (match.Success)
            {
                lastMatchPos = match.Index;
                lastMatchLen = match.Value.Length;
                stringTokens.Enqueue(match.Value.Trim());
                match = match.NextMatch();
            }
            //todo: all token errors with displayed in strings; continue until end of expression? two regexes?
            if (lastMatchPos + lastMatchLen < expression.Length)
                throw new ArgumentException(String.Format("Invalid token at position {0}", lastMatchLen + lastMatchPos));
            return stringTokens;
        }
        #endregion


    }
}
