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
            var fixLength = FixLength(first, second);
            first = fixLength.Item1;
            second = fixLength.Item2;

            var firstAscii = ASCIIEncoding.ASCII.GetBytes(first); 
            var secondAscii = ASCIIEncoding.ASCII.GetBytes(second);

            var xor = ExecuteXor(firstAscii, secondAscii);

            return ASCIIEncoding.ASCII.GetString(xor);
        }

        public static string ExecuteXorBase64(string first, string second)
        {
            var fixLength = FixLength(first, second);
            first = fixLength.Item1;
            second = fixLength.Item2;

            var firstBase64 = Base64Encoder.GetBase64Bytes(first);
            var secondBase64 = Base64Encoder.GetBase64Bytes(second);

            var xor = ExecuteXor(firstBase64, secondBase64);

            return Base64Encoder.GetString(xor);
        }

        public static byte[] ExecuteXor(byte[] firstBytes, byte[] secondBytes)
        {
            var xor = new byte[firstBytes.Length];
            for (int i = 0; i < firstBytes.Length; i++)
                xor[i] = (byte)(firstBytes[i] ^ secondBytes[i]);

            #region Debug output 
            //foreach (var b in xor)
            //    Console.Write(b ? 1 : 0);
            //Console.WriteLine();
            #endregion
            return xor;
        }

        private static Tuple<string, string> FixLength(string first, string second)
        {
            if (string.IsNullOrEmpty(first))
                first = "";
            if(string.IsNullOrEmpty(second))
                second = "";

            while (first.Length < second.Length)
            {
                first += '0';
            }

            while (second.Length < first.Length)
            {
                second += '0';
            }

            return new Tuple<string, string>(first, second);
        }
    }
}
