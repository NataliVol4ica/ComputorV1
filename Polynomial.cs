using System;
using System.Linq;
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
            Equation,
            Var
        }
        protected struct PolyToken
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
           new Regex(@"\G\s*(\d+(\.\d+)?|[xX]|\+|-|\*|\^|=)\s*", RegexOptions.Compiled);
        #endregion

        #region Public
        public List<double> Parse(int degree, string expression)
        {
            Queue<string> stringTokens = Tokenize(expression);
            List<PolyToken> tokens = RecognizeLexems(stringTokens);
            CompileExpression(degree, tokens, out List<double> coefficients);
            //syntax analys
            //simplify
            return coefficients;
        }
        #endregion
        #region Protected
        private Queue<string> Tokenize(string expression)
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
        private List<PolyToken> RecognizeLexems(Queue<string> stringTokens)
        {
            var tokenQueue = new List<PolyToken>();
            TokenType tokenType;
            foreach (var token in stringTokens)
            {
                if (token == "+" || token == "-" || token == "*")
                    tokenType = TokenType.Operator;
                else if (token == "^")
                    tokenType = TokenType.Pow;
                else if (token == "=")
                    tokenType = TokenType.Equation;
                else if (token == "x" || token == "X")
                    tokenType = TokenType.Var;
                else
                    tokenType = TokenType.Number;
                tokenQueue.Add(new PolyToken(token, tokenType));
            }
            return tokenQueue;
        }
        private void CompileExpression(int degree, List<PolyToken> tokens, out List<double> coefficients)
        {
            coefficients = new List<double>();
            coefficients.AddRange(Enumerable.Repeat(0.0, degree));
            double coeff;
            int pow;
            int sign;
            int tokenIndex = 0;
            bool metEquation = false;
            while (tokenIndex < tokens.Count)
            {
                sign = 1;
                if (tokens[tokenIndex].tokenType == TokenType.Equation)
                {
                    if (metEquation)
                        throw new Exception("Expression cannot have two equations");
                    metEquation = true;
                    tokenIndex++;
                    continue;
                }
                while (tokenIndex < tokens.Count && tokens[tokenIndex].tokenType == TokenType.Operator)
                {
                    if (tokens[tokenIndex].str == "*")
                        throw new Exception("invalid token \"*\"");
                    if (tokens[tokenIndex].str == "-")
                        sign = -sign;
                    tokenIndex++;
                }
                //znaki
                //chislo
                //umnojenie
                //x
                //pow
                //chislo 0, 1 ili 2
                if (tokenIndex == tokens.Count)
                    throw new Exception("Expression cannot end with operator");
                coefficients[pow] += coeff;
            }
        }
        #endregion


    }
}
