using System;
using System.Collections.Generic;
using System.Text;

namespace lab4_2
{
    public class Poliz
    {
        public static string ConvertToPolishNotation(string expression)
        {
            var result = new StringBuilder();
            var operatorStack = new Stack<char>();
            var allowedOperators = new HashSet<char> { '+', '-', '*', '/', '^', '(', ')' };

            foreach (var c in expression)
            {
                // Пропускаем недопустимые символы
                if (!char.IsLetterOrDigit(c) && !allowedOperators.Contains(c) && !char.IsWhiteSpace(c))
                {
                    throw new ArgumentException("Недопустимый символ:" + c);
                }

                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    result.Append(c);
                    continue;
                }

                switch (c)
                {
                    case '(':
                        operatorStack.Push(c);
                        continue;
                    case ')':
                        {
                            while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                            {
                                result.Append(operatorStack.Pop());
                            }

                            if (operatorStack.Count == 0 || operatorStack.Peek() != '(')
                            {
                                throw new InvalidOperationException("Некорректное выражение: несбалансированные скобки");
                            }

                            operatorStack.Pop();
                            continue;
                        }
                }

                while (operatorStack.Count > 0 && GetPrecedence(operatorStack.Peek()) >= GetPrecedence(c))
                {
                    result.Append(operatorStack.Pop());
                }

                operatorStack.Push(c);
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == '(' || operatorStack.Peek() == ')')
                {
                    throw new InvalidOperationException("Некорректное выражение: несбалансированные скобки");
                }

                result.Append(operatorStack.Pop());
            }

            return result.ToString().Trim();
        }

        private static int GetPrecedence(char c)
        {
            switch (c)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                case '-':
                    return 2;
                case '*':
                case '/':
                    return 3;
                case '^':
                    return 4;
                default:
                    return 5;
            }
        }
    }

}
