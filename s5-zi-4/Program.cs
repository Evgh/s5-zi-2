using System;

namespace s5_zi_4
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteLab4Routine();
        }

        private static void ExecuteLab4Routine()
        {
            Console.WriteLine("Lab 4");

            string message = "e";
            var hamming = new HammingMessage(message);

            Console.WriteLine("Порождающая матрица: ");
            hamming.HammingMatrix.Print();

            Console.WriteLine("Биты сообщения:");
            hamming.PrintMessage();

            Console.WriteLine("Биты с проверочными битами:");
            hamming.PrintEncodedMessage();

            Console.WriteLine("------------------------------------------------------------------------------");

            hamming.SendMessageWithErrors(0);
            Console.WriteLine("____________");

            hamming.SendMessageWithErrors(1);
            Console.WriteLine("____________");
            
            hamming.SendMessageWithErrors(2);
            Console.WriteLine("____________");
        }
    }
}
