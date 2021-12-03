using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_10
{
    public class CodingTriad
    {
        public CodingTriad() : this(0, 0, null)
        {
        }

        public CodingTriad(int position, int amount, char? symbol)
        {
            Position = position;
            Amount = amount;
            Symbol = symbol;
        }

        public int Position { get; set; }
        public int Amount { get; set; }
        public char? Symbol { get; set; }

        public override string ToString()
        {
            return $"[{Position+1}, {Amount}, {Symbol}]";
        }
    }
}
