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

            foreach (var c in expression)
            {
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
                                result.Append(' ');
                            }

                            if (operatorStack.Count == 0 || operatorStack.Peek() != '(')
                            {
                                Console.WriteLine("Некорректное выражение");
                                Environment.Exit(0);
                            }

                            operatorStack.Pop();
                            continue;
                        }
                }

                while (operatorStack.Count > 0 && GetPrecedence(operatorStack.Peek()) >= GetPrecedence(c))
                {
                    result.Append(operatorStack.Pop());
                    result.Append(' ');
                }

                operatorStack.Push(c);
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == '(' || operatorStack.Peek() == ')')
                {
                    Console.WriteLine("Некорректное выражение");
                    Environment.Exit(0);
                }

                result.Append(operatorStack.Pop());
                result.Append(' ');
            }

            return result.ToString();
        }

        private static int GetPrecedence(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    return 0;
            }
        }
    }
    


}
