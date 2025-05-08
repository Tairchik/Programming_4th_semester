namespace lab4_2
{
    internal static class Program
    {

        static void Main()
        {
            Poliz pn = new();
            string input = "3 + 4 * 2 / (1 - 5)^2";
            Console.WriteLine("Input - " + input);
            string result = Poliz.ConvertToPolishNotation(input);
            Console.Write("Postfix Notation: ");
            Console.Write(result);

        }
    }
}