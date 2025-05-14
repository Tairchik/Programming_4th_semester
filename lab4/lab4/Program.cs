namespace lab4
{
    internal static class Program
    {

        static void Main()
        {
            ShadowLine shadowLine = new ShadowLine(new int[,] { {1, 2 }, {4, 8 }, {5, 7} });
            Console.WriteLine(shadowLine.CalculateSum());
        }
    }
}