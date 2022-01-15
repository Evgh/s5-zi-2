using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLogic;
using s5_zi_2.Exceptions;

namespace s5_zi_2
{
    public class Alphabet
    {
        public readonly int AlphabetLength;

        private char[] _alphabet;
        private List<SuperSymbol> _alphabetWithStatistics;
        
        public double EntropyShennon { get; set; }
        public double EntropyChartley { get; set; }
        public double EntropyBynary { get; set; }
        public double Redundancy { get; set; }

        public double EffectiveEntropy { get; set; }

        public Alphabet(string alphabet)
        {
            EntropyShennon = 0;
            EntropyChartley = 0;
            EntropyBynary = 0;

            _alphabet = alphabet.ToCharArray();
            AlphabetLength = _alphabet.Length;
        }

        #region Public Methods

        public void CalculateAllEntropies(string baseForStatistics)
        {
            CalculateEntropyChennon(baseForStatistics);
            CalculateEntropyChartley();
            CalculateBynaryEntropy(baseForStatistics);

            CalculateRedundancy();
        }

        public void CalculateEntropyChennon(string baseForStatistics)
        {
            CalculateAlphabetStatistics(baseForStatistics);
            CalculateEntropyShennonFromStatistics();
        }

        public void CalculateEntropyChartley()
        {
            EntropyChartley = Math.Log(_alphabet.Length, 2);
        }

        public void CalculateBynaryEntropy(string text)
        {
            int bitZeroCount;
            int bitOneCount;
            int length;

            length = CountBitsOccurences(text, out bitOneCount, out bitZeroCount);
            CalculateBynaryEntropy(bitOneCount, bitZeroCount, length);
        }

        public void CalculateEffectiveEntropy(double errorChance)
        {
            EffectiveEntropy = (1 - (-errorChance * Math.Log(errorChance, 2) - (1 - errorChance) * Math.Log(1 - errorChance, 2)));
        }

        #endregion

        #region Private Methods

        private void CalculateRedundancy()
        {
            Redundancy = (EntropyChartley == 0) ? 0 : ((EntropyChartley - EntropyShennon) / EntropyChartley) * 100;
        }

        private void CalculateBynaryEntropy(double bitOneCount, double bitZeroCount, double length)
        {
            EntropyBynary = - (bitOneCount / length * Math.Log(bitOneCount / length, 2) + bitZeroCount / length * Math.Log(bitZeroCount / length, 2));
        }

        private void CalculateEntropyShennonFromStatistics()
        {
            if (_alphabetWithStatistics == null)
                throw new StatisticsIsNotGatheredException();

            foreach (var symbol in _alphabetWithStatistics)
                if (symbol.Chance != 0)
                    EntropyShennon += -symbol.Chance * Math.Log(symbol.Chance, 2);            
        }

        private void CalculateAlphabetStatistics(string baseForStatistics)
        {
            _alphabetWithStatistics = new List<SuperSymbol>();

            char[] workText = baseForStatistics.ToCharArray();
            int accurateTextLength = CountAlphabetSymbolsOccurence(workText);

            for (int i = 0; i < _alphabet.Length; i++)
            {
                SuperSymbol temp = new SuperSymbol { Symbol = _alphabet[i] };
                temp.AmountOfOccurrences = CountAmountOfSymbolOccurences(workText, temp.Symbol);
                temp.Chance = (double)temp.AmountOfOccurrences / (double)accurateTextLength;

                _alphabetWithStatistics.Add(temp);
            }
        }

        private int CountAlphabetSymbolsOccurence(char[] someText)
        {
            int textLength = 0;

            foreach(var symbol in someText)
                if (_alphabet.Contains(symbol))
                    textLength++;

            return textLength;
        }

        private int CountAmountOfSymbolOccurences(char[] someText, char symbol)
        {
            int numOfOccurences = 0;

            for (int y = 0; y < someText.Length; y++)
                if (_alphabet.Contains(someText[y]) && symbol == someText[y])
                    numOfOccurences++;

            return numOfOccurences;
        }

        private int CountBitsOccurences(string baseForStatistics, out int bitOneCount, out int bitZeroCount)
        {
            byte[] buf = Encoding.UTF8.GetBytes(baseForStatistics);
            var bits = BynaryEncoder.GetBitsFromBytes(buf);

            bitOneCount = 0;
            bitZeroCount = 0;

            foreach (var bit in bits)
            {
                if (bit)
                    bitOneCount++;
                else
                    bitZeroCount++;
            }

            return bits.Length;
        }

        #endregion

        #region Overriden Methods 
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append($"Alphabet: [{new string(_alphabet)}] - [{AlphabetLength}]\n");
            stringBuilder.Append($"Entropy - {EntropyShennon}\n");
            stringBuilder.Append($"Chartley entropy - {EntropyChartley}\n");
            stringBuilder.Append($"Binary entropy {EntropyBynary}\n");
            stringBuilder.Append($"Redundancy - {Redundancy}\n");

            for (int i = 0; i < _alphabetWithStatistics.Count; i++)
                stringBuilder.Append($"{_alphabetWithStatistics[i].Symbol} - {Math.Round(_alphabetWithStatistics[i].Chance, 5)} - {_alphabetWithStatistics[i].AmountOfOccurrences}\n");

            return stringBuilder.ToString();
        }
        #endregion
    }
}
