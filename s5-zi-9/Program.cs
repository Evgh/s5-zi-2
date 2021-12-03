using System;
using System.IO;
using System.Text;

namespace s5_zi_9
{
    class Program
    {
        static string ReadFile(string path)
        {
            return new StreamReader(path, Encoding.Default).ReadToEnd();
        }


        public static void Main(string[] args)
        {
            string message = "Касперович Евгения";
            Console.WriteLine();

            var alg = new Huffman();
            alg.CreateLettersInWordList(message);
            alg.HuffmanAlgorithmMethod();

            Console.WriteLine("====================================");

            string text = ReadFile(@"..\..\..\..\textfiles/russian.txt");
            string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

            alg.CreateLettersInTextList(message.ToLower(), text.ToLower(), alphabet);
            alg.HuffmanAlgorithmMethod();


            Console.ReadKey();
        }
    }
}
