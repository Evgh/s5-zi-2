using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_3
{
    public static class Encoder
    {
        static string base64alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        static char fillSymbol = '=';

        public static char[] Encode(string info)
        {
            var bytes = Encoding.ASCII.GetBytes(info);
            var bits = GetBitsFromBytes(bytes);

            var baseIndexes = GetBase64Indexes(bits);

            var messageLenth = baseIndexes.Length % 4 == 0 ? baseIndexes.Length : ((baseIndexes.Length % 4 == 2) ? baseIndexes.Length + 2 : baseIndexes.Length + 1);
            char[] message = new char[messageLenth];

            for (int i = 0; i < messageLenth; i++)
            {
                message[i] = i < baseIndexes.Length ? base64alphabet[baseIndexes[i]] : fillSymbol;
            }

            Console.WriteLine(messageLenth);
            Console.WriteLine(message);

            return message;
        }

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
#if DEBUG
            for (int i = 0; i < bits.Length; i++)
            {
                Console.Write(bits[i] ? 1 : 0);
                if ((i + 1) % 8 == 0)
                    Console.Write(" ");
            }
            Console.WriteLine();

            for (int i = 0; i < bits.Length; i++)
            {
                Console.Write(bits[i] ? 1 : 0);
                if ((i + 1) % 6 == 0)
                    Console.Write(" ");
            }
            Console.WriteLine();
#endif
            #endregion
            return bits;
        }


        static int[] GetBase64Indexes(bool[] bits)
        {
            var indAmount = bits.Length % 24 == 0 ? bits.Length / 6 : bits.Length / 6 + 1;
            var indexes = new int[indAmount];

            for (int i = 0; i < indexes.Length; i++)
            {
                int index = 0;
                for (int j = 0; j < 6; j++)
                {
                    int place = i * 6 + 5 - j;
                    // заполняем нулями справа, если число битов не делится ровно на 6
                    if (place < bits.Length)
                        index += bits[place] ? (1 << j) : 0;
                }
                indexes[i] = index;
            }
            #region Debug output
#if DEBUG
            Console.WriteLine(indAmount);
            foreach (var i in indexes)
                Console.Write(i + " ");
            Console.WriteLine();
#endif
            #endregion
            return indexes;
        }

    }
}
