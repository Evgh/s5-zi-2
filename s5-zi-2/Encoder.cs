using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_2
{
    public static class Encoder
    {
        public static bool[] GetBitsFromBytes(byte[] bytes)
        {
            var bits = new bool[bytes.Length * 8];

            for (int i = 0; i < bytes.Length; i++)
            {
                int buff = bytes[i];
                for (int j = 0; j < 8; j++)
                {
                    bits[8 * i + 7 - j] = (buff & 1) == 1;
                    buff = buff >> 1;
                }
            }
            #region Debug output
            //for (int i = 0; i < bits.Length; i++)
            //{
            //    Console.Write(bits[i] ? 1 : 0);
            //    if ((i + 1) % 8 == 0)
            //        Console.Write(" ");
            //}
            //Console.WriteLine();

            //for (int i = 0; i < bits.Length; i++)
            //{
            //    Console.Write(bits[i] ? 1 : 0);
            //    if ((i + 1) % 6 == 0)
            //        Console.Write(" ");
            //}
            //Console.WriteLine();

            #endregion
            return bits;
        }
    }
}
