using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

//todo: remember position of lexems to show the place of error?

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
        public List<double> Parse(string expression)
        {
            Queue<string> stringTokens = Tokenize(expression);
            List<PolyToken> tokens = RecognizeLexems(stringTokens);
            CompileExpression(tokens, out List<double> coefficients);
            //syntax analys
            //simplify
            return coefficients;
        }
        public void Write(List<double> poly)
        {
            bool isFirst = true;
            bool wroteCoef;
            for (int i = 0; i < poly.Count; i++)
            {
                if (poly[i] != 0.0)
                {
                    if (isFirst)
                    {
                        if (poly[i] < 0)
                        {
                            Console.Write("-");
                            poly[i] = -poly[i];
                        }
                        isFirst = false;
                    }
                    else
                    {
                        if (poly[i] < 0)
                        {
                            Console.Write("-");
                            poly[i] = -poly[i];
                        }
                        else
                            Console.Write(" + ");
                    }
                    wroteCoef = false;
                    if (i == 0 || (i > 0 && poly[i] != 1.0))
                    {
                        Console.Write(poly[i]);
                        wroteCoef = true;
                    }
                    if (i == 0)
                        continue;
                    if (wroteCoef)
                        Console.Write("*");
                    Console.Write("X");
                    if (i == 1)
                        continue;
                    Console.Write("^" + i);
                }
            }
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
        private void CompileExpression(List<PolyToken> tokens, out List<double> coefficients)
        {
            coefficients = new List<double>();
            coefficients.AddRange(Enumerable.Repeat(0.0, 2));
            double coeff;
            int pow;
            double doublePow;
            int sign;
            int tokenIndex = 0;
            int numOfOperators;
            bool isStart = true;
            bool metEquation = false;
            while (tokenIndex < tokens.Count)
            {
                if (tokens[tokenIndex].tokenType == TokenType.Equation)
                {
                    if (metEquation)
                        throw new Exception("Expression cannot have two equations");
                    metEquation = true;
                    isStart = true;
                    tokenIndex++;
                    if (tokenIndex == tokens.Count)
                        throw new Exception("Expression is missing it's right part");
                }
                sign = metEquation ? -1 : 1;
                numOfOperators = 0;
                while (tokenIndex < tokens.Count && tokens[tokenIndex].tokenType == TokenType.Operator)
                {
                    if (tokens[tokenIndex].str == "*")
                        throw new Exception("invalid token \"*\"");
                    if (tokens[tokenIndex].str == "-")
                        sign = -sign;
                    tokenIndex++;
                    numOfOperators++;
                }
                if (tokenIndex == tokens.Count)
                    throw new Exception("Expression cannot be ended by operator");
                if (!isStart && numOfOperators == 0)
                    throw new Exception("Expression is missing operator");
                isStart = false;
                if (tokens[tokenIndex].tokenType == TokenType.Number)
                {
                    Double.TryParse(tokens[tokenIndex++].str, out coeff);
                    if (tokenIndex == tokens.Count)
                        break;
                    if (tokens[tokenIndex].str != "*") //esli tolko 4islo
                    {
                        coefficients[0] += sign * coeff;
                        sign = 1;
                        continue;
                    }
                    else
                        tokenIndex++;
                    if (tokenIndex == tokens.Count)
                        break;
                }
                else
                    coeff = 1;
                if (tokens[tokenIndex].tokenType == TokenType.Number)
                {
                    Double.TryParse(tokens[tokenIndex++].str, out coeff);
                    if (tokenIndex == tokens.Count)//esli 4islo v konce
                    {
                        coefficients[0] += sign * coeff;
                        sign = 1;
                        break;
                    }
                    if (tokens[tokenIndex].str != "*") //esli tolko 4islo
                    {
                        coefficients[0] += sign * coeff;
                        sign = 1;
                        continue;
                    }
                    if (++tokenIndex == tokens.Count)
                        break;

                }
                else
                    coeff = 0;

                if (tokens[tokenIndex].tokenType == TokenType.Var) //esli dalshe idet x
                {
                    tokenIndex++;
                    if (tokenIndex == tokens.Count) //esli prosto x v konce
                    {
                        coefficients[1] += sign * coeff;
                        sign = 1;
                        break;
                    }
                    if (tokens[tokenIndex].str != "^") //esli tolko x
                    {
                        coefficients[0] += sign * coeff;
                        sign = 1;
                        continue;
                    }
                    if (++tokenIndex == tokens.Count)
                        break;
                    if (tokens[tokenIndex].str.Contains("."))
                        throw new Exception(String.Format("Pow has to be integer. {0} is not.", tokens[tokenIndex].str));
                    if (tokens[tokenIndex].str.Length > 1)
                        throw new Exception(String.Format("Pow has to be 0..2. {0} is not.", tokens[tokenIndex].str));
                    Double.TryParse(tokens[tokenIndex].str, out doublePow);
                    pow = (int)doublePow;
                    if (pow < 0 || pow > 2)
                        throw new Exception(String.Format("Pow has to be 0..2. {0} is not.", tokens[tokenIndex].str));
                    coefficients[0] += sign * coeff;
                    sign = 1;
                    tokenIndex++;
                }
                else
                    throw new Exception("Expression is missing X^N");
            }
            if (!metEquation)
                throw new Exception("Expression is missing \"=\"");
        }
        #endregion

        //todo: make SyntaxException
    }
}
