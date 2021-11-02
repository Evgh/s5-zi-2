using System;
using s5_zi_2;

namespace s5_zi_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var info = FileSlave.ReadFileToEnd(@"textfiles/english.txt");

            Console.WriteLine(info);
            Console.WriteLine(Base64Encoder.Encode(info));

        }
    }
}
