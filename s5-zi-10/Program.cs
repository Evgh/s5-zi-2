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


            var encoded = LempelZivWelcher.Encode(message, bufferSize, windowSize, true);

            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine($"Message:{message}");
            Console.WriteLine($"Encoded: {string.Join('|', encoded)}");
            Console.WriteLine($"Decoded: {LempelZivWelcher.Decode(encoded, bufferSize)}");

            Console.WriteLine("-------------------------------------------------------------------------------");

            message = "01010101111010101010101010010100101010010101001100101010101010111110101010101010101010101010010101010100101111111110101111111110000000000";
            encoded = LempelZivWelcher.Encode(message, 5, 5);
            Console.WriteLine($"Message:{message}");
            Console.WriteLine($"Encoded (len = {encoded.Count}): {string.Join('|', encoded)}");
            Console.WriteLine($"Decoded: {LempelZivWelcher.Decode(encoded, 5)}");

            Console.WriteLine("-------------------------------------------------------------------------------");
            encoded = LempelZivWelcher.Encode(message, 25, 25);
            Console.WriteLine($"Message:{message}");
            Console.WriteLine($"Encoded (len = {encoded.Count}): {string.Join('|', encoded)}");
            Console.WriteLine($"Decoded: {LempelZivWelcher.Decode(encoded, 25)}");

            Console.WriteLine("-------------------------------------------------------------------------------");
            encoded = LempelZivWelcher.Encode(message, 100, 100);
            Console.WriteLine($"Message:{message}");
            Console.WriteLine($"Encoded (len = {encoded.Count}): {string.Join('|', encoded)}");
            Console.WriteLine($"Decoded: {LempelZivWelcher.Decode(encoded, 100)}");

        }
    }
}
