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
            Console.WriteLine("___");

            AlphabetRoutine("абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
                            @"textfiles\russian.txt",
                            "КасперовичЕвгенияНиколаевна"
                            );
            Console.ReadLine();
        }

        private static void AlphabetRoutine(string alphabetStr, string source, string strInputEng)
        {
            Alphabet alphabet = new Alphabet(alphabetStr);
            string text = ReadFile(source);

            alphabet.CountEntropy(text.ToLower());
            alphabet.PrintAllData();
            alphabet.PrintASCII(strInputEng.ToLower());
            alphabet.PtintAlphabetInfo(strInputEng.ToLower());

            Console.WriteLine("Количество информации с вероятностью ошибочной передачи 0.1:" + alphabet.GetEffectiveEntropy(0.1) * strInputEng.Length);
            Console.WriteLine("Количество информации с вероятностью ошибочной передачи 0.5:" + alphabet.GetEffectiveEntropy(0.5) * strInputEng.Length);
            Console.WriteLine("Количество информации с вероятностью ошибочной передачи 1:" + Math.Round(alphabet.GetEffectiveEntropy(0.999999) * strInputEng.Length));
        }

        static private string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
