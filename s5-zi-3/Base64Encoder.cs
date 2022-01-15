using System.Text;
using CommonLogic;

namespace s5_zi_3
{
    public static class Base64Encoder
    {
        static string _base64alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        static char _fillSymbol = '=';

        public static string Base64Alphabet => _base64alphabet;
        public static char FillSymbol => _fillSymbol;

        public static string Encode(string info)
        {
            var bytes = Encoding.ASCII.GetBytes(info);
            var bits = BynaryEncoder.GetBitsFromBytes(bytes);
            var baseIndexes = GetBase64Indexes(bits);
            var message = GetString(baseIndexes);

            return message;
        }

        public static byte[] GetBase64Bytes(string text)
        {
            byte[] bytes = new byte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                if (_base64alphabet.Contains(text[i]))
                    bytes[i] = (byte)_base64alphabet.IndexOf(text[i]);
                else
                    bytes[i] = 65;
            }
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            var messageLenth = ConvertBase64LengthToNormal(bytes.Length);
            char[] message = new char[messageLenth];

            for (int i = 0; i < messageLenth; i++)
            {
                if (i >= bytes.Length)
                    message[i] = _fillSymbol;

                else if (bytes[i] >= _base64alphabet.Length)
                    message[i] = _fillSymbol;

                else
                    message[i] = _base64alphabet[bytes[i]];
            }
            return new string(message);
        }

        private static byte[] GetBase64Indexes(bool[] bits)
        {
            var indAmount = ConvertMessageLengthToBase64(bits.Length);
            var indexes = new byte[indAmount];

            for (int i = 0; i < indexes.Length; i++)
            {
                byte index = 0;
                for (int j = 0; j < 6; j++)
                {
                    int place = i * 6 + 5 - j;
                    // заполняем нулями справа, если число битов не делится ровно на 6
                    if (place < bits.Length)
                        index += (byte)(bits[place] ? (1 << j) : 0);
                }
                indexes[i] = index;
            }

            return indexes;
        }

        private static int ConvertBase64LengthToNormal(int base64length)
        {
            return base64length % 4 == 0 ?
                        base64length :
                        ((base64length % 4 == 2) ?
                                base64length + 2 :
                                base64length + 1);
        }

        private static int ConvertMessageLengthToBase64(int messageLength)
        {
            return messageLength % 24 == 0 ? messageLength / 6 : messageLength / 6 + 1;
        }
    }
}
