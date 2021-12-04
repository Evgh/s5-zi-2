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
        private Dictionary<string, string> codes;
        private Dictionary<string, string> revertCodes;


        public void ConsoleH()
        {
            Console.WriteLine();

            foreach(var s in lettersTree)
            {
                Console.WriteLine(s.values + "  -  " + s.chance);
            }

            //lettersTree.ForEach(s => Console.WriteLine(s.values + "  -  " + s.chance));
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
                if (!arrText.Contains(alphabet[i]))
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

        public string HuffmanAlgorithmMethod(string message)
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

            codes = new Dictionary<string, string>();
            revertCodes = new Dictionary<string, string>();

            for (int i = 0; i < asd.Count; i++)
            {
                HuffmanBranch temp = asd[i];
                (string, string) tempTuple = (string.Empty, string.Empty);

                //Console.Write("\n" + temp.values + " - ");
                tempTuple.Item1 = temp.values;

                for (; temp.branch != null; temp = temp.branch)
                {
                    //Console.Write(temp.bynary_value + " ");
                    tempTuple.Item2 = string.Concat(tempTuple.Item2 ?? string.Empty, temp.bynary_value);
                }

                codes.Add(tempTuple.Item1, tempTuple.Item2);
                revertCodes.Add(tempTuple.Item2, tempTuple.Item1);
            }
            Console.WriteLine(string.Join('\n', codes));
            Console.WriteLine();

            var newMessage = string.Empty;
            foreach(var letter in message)
            {
                Console.WriteLine($"{letter} - {codes[$"{letter}"]}");
                newMessage = string.Concat(newMessage, codes[$"{letter}"]);
            }

            Console.WriteLine(newMessage);

            return newMessage;
        }

        public void Decode(string encoded)
        {
            string temp = string.Empty;
            for (int i = 0; i < encoded.Length; i++)
            {
                temp = string.Concat(temp, encoded[i]);
                if (revertCodes.ContainsKey(temp))
                {
                    Console.WriteLine(revertCodes[temp]);
                    temp = string.Empty;
                }
            } 
        }
    }
}
