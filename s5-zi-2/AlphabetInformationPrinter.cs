﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_2
{
    public class AlphabetInformationPrinter
    {
        protected readonly Alphabet _alphabet;

        public AlphabetInformationPrinter(Alphabet alphabet)
        {
            _alphabet = alphabet;
        }

        public void PrintAlphabetData()
        {
            Console.WriteLine(_alphabet);
        }

        public void PrintASCIIInfoAmount(string str)
        {
            Console.WriteLine($"ASCII: {str.Length * _alphabet.EntropyBynary * 8} ");
        }

        public void PtintInfoAmount(string str)
        {
            Console.WriteLine($"Info amount: {str.Length * _alphabet.EntropyShennon}");
        }

        public void PrintInfoAmountWithErrorChance(string str, double errorChance)
        {
            _alphabet.CalculateEffectiveEntropy(errorChance);
            Console.WriteLine($"Error chance: {errorChance},  Info amount: {str.Length * _alphabet.EffectiveEntropy}");
        }
    }
}