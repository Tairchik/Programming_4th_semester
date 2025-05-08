namespace lab4
{
    internal static class Program
    {

        static void Main()
        {
            ShadowLine shadowLine = new ShadowLine(new double[,] { {-2, -3 } });
            Console.WriteLine(shadowLine.CalculateSum());
        }
    }
}