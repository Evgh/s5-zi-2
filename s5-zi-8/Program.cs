using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace s5_zi_8
{
    class Program
    {
        public static string[] CreateMatrixWord(string word, out string keyWord, out int numColumnWord)
        {
            List<string> listWordShift = new List<string>();
            string shiftWord = word;

            listWordShift.Add(word);
            for (int iter = 1; iter < word.Length; iter++)
            {
                shiftWord = shiftWord.Substring(1, shiftWord.Length - 1) + shiftWord[0];
                listWordShift.Add(shiftWord);
            }
            listWordShift.Sort();

            keyWord = "";
            numColumnWord = 0;
            for (int i = 0; i < listWordShift.Count; i++)
            {
                keyWord = keyWord + listWordShift[i][listWordShift[i].Length - 1];

                for (int y = 0, repeat = 0; y < listWordShift[i].Length; y++)
                {
                    if (listWordShift[i][y] == word[y])
                    {
                        repeat++;
                    }
                    else
                    {
                        break;
                    }
                    if (repeat == word.Length)
                    {
                        numColumnWord = i;
                    }
                }
            }

            return listWordShift.ToArray();
        }

        public static string[] GetMatrixKeyWord(string keyWord, int numColumnWord, out string word)
        {
            List<string> decodingListWordShift = new List<string>();

            for (int i = 0; i < keyWord.Length; i++)
            {
                decodingListWordShift.Add("");
            }

            for (int i = 0; i < keyWord.Length; i++)
            {
                for (int y = 0; y < keyWord.Length; y++)
                {
                    decodingListWordShift[y] = keyWord[y] + decodingListWordShift[y];
                }

                decodingListWordShift.Sort();
            }
            word = decodingListWordShift[numColumnWord];

            return decodingListWordShift.ToArray();
        }

        public static void ConsoleWriteTask(string task)
        {
            string[] temp;

            string keyWord;
            int numColumnWord;

            Stopwatch watch = Stopwatch.StartNew();
            temp = CreateMatrixWord(task, out keyWord, out numColumnWord);
            watch.Stop();
            Console.WriteLine("encoding time: " + watch.Elapsed);

            Console.WriteLine("\nX.k = ' " + task + " '\n");
            for (int i = 0; i < temp.Length; i++)
            {
                Console.Write(temp[i]);
                if (i == numColumnWord)
                {
                    Console.WriteLine("   <---- " + i);
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\nX.n = ' " + keyWord + " ', " + numColumnWord + "\n");


            string decodingWord;

            watch = Stopwatch.StartNew();
            temp = GetMatrixKeyWord(keyWord, numColumnWord, out decodingWord);
            watch.Stop();
            Console.WriteLine("decoding time: " + watch.Elapsed + "\n");

            for (int i = 0; i < temp.Length; i++)
            {
                Console.Write(temp[i]);
                if (i == numColumnWord)
                {
                    Console.WriteLine("   <---- " + i);
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\ndecoded: " + decodingWord);
            Console.WriteLine("====================================================================");
        }

        static void Main(string[] args)
        {
            string task1 = "Евгения";
            string task2 = "Касперович";
            string task3 = "Бипки";
            string task4 = "111011111110111011101011";

            ConsoleWriteTask(task1);
            ConsoleWriteTask(task2);
            ConsoleWriteTask(task3);
            ConsoleWriteTask(task4);
        }
    }
}
