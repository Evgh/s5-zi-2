using System;
using System.Text;

namespace s5_zi_3
{
    public static class XOR
    {
        public static string ExecuteXorAscii(string value, string key)
        {
            return ExecuteXor(value, key, ASCIIEncoding.ASCII.GetBytes, ASCIIEncoding.ASCII.GetString);
        }

        public static string ExecuteXorBase64(string value, string key)
        {
            return ExecuteXor(value, key, Base64Encoder.GetBase64Bytes, Base64Encoder.GetString);
        }

        public static string ExecuteXor(string value, string key, Func<string, byte[]> getBytes = null, Func<byte[], string> getString = null)
        {
            if (getBytes == null || getString == null)
            {
                getBytes = ASCIIEncoding.ASCII.GetBytes;
                getString = ASCIIEncoding.ASCII.GetString;
            }

            Tuple<string, string> lengthFixed = FixLength(value, key);
            value = lengthFixed.Item1;
            key = lengthFixed.Item2;

            var valueBytes = getBytes?.Invoke(value);
            var keyBytes = getBytes?.Invoke(key);

            var xor = ExecuteXor(valueBytes, keyBytes);

            return getString?.Invoke(xor);
        }

        public static byte[] ExecuteXor(byte[] valueBytes, byte[] keyBytes)
        {
            var xor = new byte[valueBytes.Length];

            for (int i = 0; i < valueBytes.Length; i++)
                xor[i] = (byte)(valueBytes[i] ^ keyBytes[i]);

            return xor;
        }

        private static Tuple<string, string> FixLength(string value, string key)
        {
            if (string.IsNullOrEmpty(value))
                value = "";
            if(string.IsNullOrEmpty(key))
                key = "";

            while (value.Length < key.Length)
            {
                value += '0';
            }

            while (key.Length < value.Length)
            {
                key += '0';
            }

            return new Tuple<string, string>(value, key);
        }
    }
}
