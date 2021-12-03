using System;

namespace s5_zi_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var bufferSize = 10;
            var windowSize = 10;
            var message = "антантоааааантон";


            var encoded = LempelZivWelcher.Encode(message, bufferSize, windowSize);

            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine($"Message:{message}");
            Console.WriteLine($"Encoded: {string.Join('|', encoded)}");

            Console.WriteLine($"Decoded: {LempelZivWelcher.Decode(encoded, bufferSize)}");
        }
    }
}
