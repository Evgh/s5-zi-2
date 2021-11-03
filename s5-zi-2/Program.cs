using System;
using System.IO;

namespace s5_zi_2
{
    class Program
    {
        static void Main(string[] args)
        {
            AlphabetRoutine("abcdefghijklmnopqrstuvwxyz",
                            @"textfiles/english.txt",
                            "KasperovichEugeniyaNikolaevna"
                            );

            AlphabetRoutine("абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
                            @"textfiles\russian.txt",
                            "КасперовичЕвгенияНиколаевна"
                            );

            AlphabetRoutine("01", @"textfiles/binary.txt", "11001010 11000101");

            Console.ReadLine();
        }

        private static void AlphabetRoutine(string alphabetStr, string source, string strInput)
        {
            Alphabet alphabet = new Alphabet(alphabetStr);
            string text = FileSlave.ReadFileToEnd(source);

            alphabet.CountAllEntropies(text);

            alphabet.PrintAllData();
            alphabet.PrintASCIIInfoAmount(strInput.ToLower());
            alphabet.PtintInfoAmount(strInput.ToLower());

            alphabet.PrintInfoAmount(strInput.ToLower(), 0.1);
            alphabet.PrintInfoAmount(strInput.ToLower(), 0.5);
            alphabet.PrintInfoAmount(strInput.ToLower(), 0.9999);
            //alphabet.PrintInfoAmount(strInput.ToLower(), 1.0);

            Console.WriteLine("___");
            Console.WriteLine();
        }
    }
}
