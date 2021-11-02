using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_2
{
    public class Alphabet
    {
        private char[] _alphabet;
        private List<SuperSymbol> _allSymStat;
        public readonly int N;
        
        public double Entropy { get; set; }
        public double BynaryEntropy { get; set; }

        public Alphabet(string setAlphabet)
        {
            Entropy = 0;
            BynaryEntropy = 0;

            _alphabet = setAlphabet.ToCharArray();
            N = _alphabet.Length;
        }

        public void CountEntropy(string text)
        {
            _allSymStat = new List<SuperSymbol>();

            char[] arrText = text.ToCharArray();
            int textLength = 0;
            int buff;

            for (int y = 0; y < arrText.Length; y++)
                if (Char.IsLetter(arrText[y]) || int.TryParse($"{arrText[y]}", out buff))  
                    textLength++;

            for (int i = 0; i < _alphabet.Length; i++)
            {
                SuperSymbol temp = new SuperSymbol { Symbol = _alphabet[i] };

                for (int y = 0; y < arrText.Length; y++)
                    if ((Char.IsLetter(arrText[y]) || int.TryParse($"{arrText[y]}", out buff)) && temp.Symbol == arrText[y])
                        temp.NumOfOccurrences++;


                temp.Chance = (double)temp.NumOfOccurrences / (double)textLength;
                _allSymStat.Add(temp);

                if (temp.Chance != 0) 
                    Entropy += -temp.Chance * Math.Log(temp.Chance, 2);
            }

            CountBynaryEntropy(text);
        }

        private void CountBynaryEntropy(string text)
        {
            byte[] buf = Encoding.UTF8.GetBytes(text);
            double byteZero = 0;
            double byteOne = 0;
            double lenth;

            var bits = s5_zi_2.Encoder.GetBitsFromBytes(buf);
            lenth = bits.Length;

            foreach (var bit in bits)
            {
                if (bit)
                    byteOne++;
                else
                    byteZero++;
            }

            BynaryEntropy = -(byteOne / lenth * Math.Log(byteOne / lenth, 2) + byteZero / lenth * Math.Log(byteZero / lenth));
        }
        private double GetEffectiveEntropy(double errorChance)
        {
            return (1 - (-errorChance * Math.Log(errorChance, 2) - (1 - errorChance) * Math.Log(1 - errorChance, 2)));
        }

        public void PrintAllData()
        {
            Console.WriteLine($"Alphabet: [{new string(_alphabet)}] - [{N}]");
            Console.WriteLine("Entropy - " + Entropy);
            Console.WriteLine("Binary entropy :" + BynaryEntropy);

            for (int i = 0; i < _allSymStat.Count; i++)
                Console.WriteLine(_allSymStat[i].Symbol + " - " + Math.Round(_allSymStat[i].Chance, 5) + " - " + _allSymStat[i].NumOfOccurrences);
        }

        public void PrintASCIIInfoAmount(string str)
        {
            Console.WriteLine($"ASCII: {str.Length * BynaryEntropy * 8} ");
        }

        public void PtintInfoAmount(string str)
        {
            Console.WriteLine($"Info amount: {str.Length * Entropy}");
        }

        public void PrintInfoAmount(string str, double errorChance)
        {
            double effectiveEntropy = GetEffectiveEntropy(errorChance);
            Console.WriteLine($"Error chance: {errorChance},  Info amount: {str.Length * effectiveEntropy}");
        }
    }
}
