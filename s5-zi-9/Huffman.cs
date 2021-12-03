using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_9
{
    public class Huffman
    {
        private class HuffmanBranch
        {
            public HuffmanBranch branch;

            public string values { get; set; }
            public double chance { get; set; }

            public int bynary_value { get; set; }

            public HuffmanBranch(string value)
            {
                values = value;
                bynary_value = -1;
            }
        }

        private List<HuffmanBranch> lettersTree;

        public void ConsoleH()
        {
            Console.WriteLine();
            lettersTree.ForEach(s => Console.WriteLine(s.values + "  -  " + s.chance));
        }

        public void CreateLettersInWordList(string message)
        {
            lettersTree = new List<HuffmanBranch>();
            string tmp = new string(message.Distinct().ToArray());
            for (int i = 0; i < tmp.Length; i++)
            {
                lettersTree.Add(new HuffmanBranch(tmp[i].ToString()));
                lettersTree[i].chance = Math.Round((double)(message.Split((lettersTree[i].values.ToCharArray())[0]).Count() - 1) / (double)message.Length, 3);
            }

            ConsoleH();
            lettersTree = lettersTree.OrderByDescending(u => u.chance).ToList();
            ConsoleH();
        }

        public void CreateLettersInTextList(string message, string text, string alphabet)
        {
            lettersTree = new List<HuffmanBranch>();
            HuffmanBranch temp;
            char[] arrText = text.ToCharArray();

            int textLength = 0;
            int numOfOccurrences = 0;

            for (int y = 0; y < arrText.Length; y++)
            {
                if (Char.IsLetter(arrText[y]))
                {
                    textLength++;
                }
            }

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (!message.Contains(alphabet[i]))
                {
                    continue;
                }
                temp = new HuffmanBranch(alphabet[i].ToString());

                for (int y = 0; y < arrText.Length; y++)
                {
                    if (Char.IsLetter(arrText[y]) && temp.values == arrText[y].ToString())
                    {
                        numOfOccurrences++;
                    }
                }

                temp.chance = Math.Round(numOfOccurrences / (double)textLength, 3);
                lettersTree.Add(temp);
            }

            ConsoleH();
            lettersTree = lettersTree.OrderByDescending(u => u.chance).ToList();
            ConsoleH();
        }

        public void HuffmanAlgorithmMethod()
        {
            List<HuffmanBranch> asd = new List<HuffmanBranch>();

            foreach (var s in lettersTree)
            {
                asd.Add(s);
            }

            for (; lettersTree.Count != 1;)
            {
                lettersTree[lettersTree.Count - 2].bynary_value = 1;
                lettersTree[lettersTree.Count - 1].bynary_value = 0;


                HuffmanBranch tmp = new HuffmanBranch($"{lettersTree[lettersTree.Count - 2].values}{lettersTree[lettersTree.Count - 1].values}")
                {
                    chance = lettersTree[lettersTree.Count - 2].chance + lettersTree[lettersTree.Count - 1].chance
                };

                Console.WriteLine("sum chance - " +
                    $"{lettersTree[lettersTree.Count - 2].values} - {lettersTree[lettersTree.Count - 2].bynary_value} " +
                    $"|| {lettersTree[lettersTree.Count - 1].values} - {lettersTree[lettersTree.Count - 1].bynary_value}");

                lettersTree[lettersTree.Count - 2].branch = tmp;
                lettersTree[lettersTree.Count - 1].branch = tmp;

                lettersTree.RemoveAt(lettersTree.Count - 1);
                lettersTree.RemoveAt(lettersTree.Count - 1);

                lettersTree.Add(tmp);

                lettersTree = lettersTree.OrderByDescending(u => u.chance).ToList();
            }

            for (int i = 0; i < asd.Count; i++)
            {
                HuffmanBranch temp = asd[i];

                Console.Write("\n" + temp.values + " - ");
                for (; temp.branch != null; temp = temp.branch)
                {
                    Console.Write(temp.bynary_value + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
