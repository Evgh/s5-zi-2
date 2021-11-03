using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_3
{
    public static class Base64Encoder
    {
        static string _base64alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        static char _fillSymbol = '=';

        public static string Base64Alphabet => _base64alphabet;
        public static char FillSymbol => _fillSymbol;

        public static char[] Encode(string info)
        {
            var bytes = Encoding.ASCII.GetBytes(info);
            var bits = s5_zi_2.Encoder.GetBitsFromBytes(bytes);

            var baseIndexes = GetBase64Indexes(bits);

            var messageLenth = baseIndexes.Length % 4 == 0 ? baseIndexes.Length : ((baseIndexes.Length % 4 == 2) ? baseIndexes.Length + 2 : baseIndexes.Length + 1);
            char[] message = new char[messageLenth];

            for (int i = 0; i < messageLenth; i++)
            {
                message[i] = i < baseIndexes.Length ? _base64alphabet[baseIndexes[i]] : _fillSymbol;
            }

            #region Debug output
            Console.WriteLine(bits.Length);
            Console.WriteLine(messageLenth);
            Console.WriteLine(message);
            #endregion

            return message;
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
            //Console.WriteLine(indAmount);
            //foreach (var i in indexes)
            //    Console.Write(i + " ");
            //Console.WriteLine();
            #endregion
            return indexes;
        }

    }
}
