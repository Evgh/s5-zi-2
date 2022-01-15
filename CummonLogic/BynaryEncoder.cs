using System;

namespace CummonLogic
{
    public static class BynaryEncoder
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
            return bits;
        }

        public static void PrintAsBynary(this bool[] message)
        {
            foreach (var bit in message)
                Console.Write(bit ? 1 : 0);
            Console.WriteLine();
        }
    }
}
