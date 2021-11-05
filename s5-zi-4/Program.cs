using System;

namespace s5_zi_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab 4");

            string message = "evgh";
            var hamming = new HammingMessage(message);

            Console.WriteLine("Порождающая матрица: ");
            hamming.HammingMatrix.Print();

            Console.WriteLine("Биты сообщения:");
            hamming.PrintMessage();

            Console.WriteLine("Биты с проверочными битами:");
            hamming.PrintEncodedMessage();


        }
    }
}
