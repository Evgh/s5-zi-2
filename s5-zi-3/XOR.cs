using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_3
{
    public static class XOR
    {
        public static string ExecuteXor(string first, string second, Func<string, byte[]> getBytes = null, Func<byte[], string> getString = null)
        {
            if (getBytes == null || getString == null)
            {
                getBytes = ASCIIEncoding.ASCII.GetBytes;
                getString = ASCIIEncoding.ASCII.GetString;
            }

            var fixLength = FixLength(first, second);
            first = fixLength.Item1;
            second = fixLength.Item2;

            var firstAscii = getBytes(first);
            var secondAscii = getBytes(second);

            var xor = ExecuteXor(firstAscii, secondAscii);

            return getString(xor);
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

        public static string ExecuteXorAscii(string first, string second)
        {
            return ExecuteXor(first, second, ASCIIEncoding.ASCII.GetBytes, ASCIIEncoding.ASCII.GetString);
        }

        public static string ExecuteXorBase64(string first, string second)
        {
            return ExecuteXor(first, second, Base64Encoder.GetBase64Bytes, Base64Encoder.GetString);
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
