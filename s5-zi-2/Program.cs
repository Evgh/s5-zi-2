using System;
using CummonLogic;

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

        private static void AlphabetRoutine(string alphabetStr, string source, string randomSample)
        {
            Alphabet alphabet = new(alphabetStr);
            AlphabetInformationPrinter informationAmountPrinter = new(alphabet);

            string sourceForStatisticsGeneration = source.ReadAsFileSourceFromProjectRoot();
            alphabet.CalculateAllEntropies(sourceForStatisticsGeneration);

            informationAmountPrinter.PrintAlphabetData();
            informationAmountPrinter.PrintASCIIInfoAmount(randomSample.ToLower());
            informationAmountPrinter.PtintInfoAmount(randomSample.ToLower());

            informationAmountPrinter.PrintInfoAmountWithErrorChance(randomSample.ToLower(), 0.1);
            informationAmountPrinter.PrintInfoAmountWithErrorChance(randomSample.ToLower(), 0.5);
            informationAmountPrinter.PrintInfoAmountWithErrorChance(randomSample.ToLower(), 0.9999);

            Console.WriteLine("___");
            Console.WriteLine();
        }
    }
}
