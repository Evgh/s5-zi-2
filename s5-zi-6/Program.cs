using System;
using System.Collections.Generic;
using CommonLogic;

namespace s5_zi_5
{
    class Program
    {
        private static readonly Lazy<Random> _random = new Lazy<Random>(() => new Random());
        static void Main(string[] args)
        {
            Console.WriteLine("Lab 5");

            bool[] polynom = { true, false, true, true, false, true, true };
            var message = GenerateMessage(23);
            int r = (int) (Math.Round(Math.Log(message.Length, 2), 0) + 1);

            Console.WriteLine("Сообщение: ");
            message.PrintAsBinary();
            Console.WriteLine("Порождающий полином: ");
            polynom.PrintAsBinary();

            var matrix = new Matrix(polynom, message.Length, r);
            bool[] encodedMessage = MessageMethods.EncodeMessage(polynom, message, r);
            bool[] encodedMessageWithOneFail = (bool[])encodedMessage.Clone();
            bool[] encodedMessageWithTwoFails = (bool[])encodedMessage.Clone();

            encodedMessageWithOneFail.CreateError(1);
            encodedMessageWithTwoFails.CreateError(2);

            Console.WriteLine("Сообщение с одной ошибкой:");
            encodedMessageWithOneFail.PrintAsBinary();
            Console.WriteLine("\nСообщение с двумя ошибками:");
            encodedMessageWithTwoFails.PrintAsBinary();

            encodedMessage.FindFailsAndFix(polynom, matrix.Instance, r);
            encodedMessageWithOneFail.FindFailsAndFix(polynom, matrix.Instance, r);
            encodedMessageWithTwoFails.FindFailsAndFix(polynom, matrix.Instance, r);
        }

        public static bool[] GenerateMessage(int size)
        {
            bool[] newMessage = new bool[size];
            for (int i = 0; i < newMessage.Length; i++)
            {
                newMessage[i] = _random.Value.Next(0, 2) == 1;
            }
            return newMessage;
        }
       
    }
}
