namespace lab4_2
{
    internal static class Program
    {

        static void Main()
        {
            string input = "+";
            Console.WriteLine("Input - " + input);
            string result = Poliz.ConvertToPolishNotation(input);
            Console.Write("Postfix Notation: ");
            Console.Write(result);

        }
    }
}