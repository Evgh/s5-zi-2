using System;
using CommonLogic;
using s5_zi_2.Logic;

namespace s5_zi_2
{
    public class TaskExecutor
    {
        public static void Execute(string alphabetStr, string source, string randomSample)
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
