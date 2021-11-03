using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_3
{
    public static class XOR
    {
        public static string ExecuteXorAscii(string first, string second)
        {
            while(first.Length < second.Length)
            {
                first += '0';
            }

            while (second.Length < first.Length)
            {
                second += '0';
            }

            var firstAscii = ASCIIEncoding.ASCII.GetBytes(first);
            var secondAscii = ASCIIEncoding.ASCII.GetBytes(second);

            var fBits = s5_zi_2.Encoder.GetBitsFromBytes(firstAscii);
            var sBits = s5_zi_2.Encoder.GetBitsFromBytes(secondAscii);

            var xor = new bool[fBits.Length];
            for(int i = 0; i < fBits.Length; i++)
            {
                xor[i] = fBits[i] ^ sBits[i]; 
            }

            var xorBytes = new byte[firstAscii.Length];
            for(int i = 0; i < xorBytes.Length; i++)
            {
                byte buff = 0;
                for(int j = 0; j < 7; j++)
                {
                    buff += (byte) (xor[i * 8 + 7 - j] ? (1 << j) : 0);
                }
                xorBytes[i] = buff;
            }

            #region Debug output 
            //foreach (var b in xor)
            //    Console.Write(b ? 1 : 0);
            //Console.WriteLine();

            //foreach (var b in xorBytes)
            //    Console.Write(b);
            //Console.WriteLine();
            #endregion

            return ASCIIEncoding.ASCII.GetString(xorBytes);
        }
    }
}
