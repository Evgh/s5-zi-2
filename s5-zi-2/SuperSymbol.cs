using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_2
{
    class SuperSymbol
    {
        public char Symbol { get; set; }
        public int AmountOfOccurrences { get; set; }
        public double Chance { get; set; }

        public SuperSymbol()
        {
            AmountOfOccurrences = 0;
        }

        public SuperSymbol(char _symbol) : this()
        {
            Symbol = _symbol;
        }
    }
}
