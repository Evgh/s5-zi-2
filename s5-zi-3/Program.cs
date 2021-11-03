using System;
using s5_zi_2;

namespace s5_zi_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Laba №3");
            var info = FileSlave.ReadFileToEnd(@"textfiles/english.txt");

            Lab3Routine("abcdefghijklmnopqrstuvwxyz", info);
            Lab3Routine($"{Base64Encoder.Base64Alphabet}{Base64Encoder.FillSymbol}", Base64Encoder.Encode(info));

            Console.WriteLine(XOR.ExecuteXorAscii("Kasperovich", "Eugeniya"));
            Console.WriteLine(XOR.ExecuteXorAscii(XOR.ExecuteXorAscii("Kasperovich", "Eugeniya"), "Eugeniya"));
            Console.WriteLine();

            Console.WriteLine(Base64Encoder.Encode("Kasperovich"));
            Console.WriteLine(XOR.ExecuteXorBase64(Base64Encoder.Encode("Kasperovich"), Base64Encoder.Encode("Eugeniya")));
            Console.WriteLine(XOR.ExecuteXorBase64(XOR.ExecuteXorBase64(Base64Encoder.Encode("Kasperovich"), Base64Encoder.Encode("Eugeniya")), Base64Encoder.Encode("Eugeniya")));
        }

        private static void Lab3Routine(string alphabetStr, string text)
        {
            Alphabet alphabet = new Alphabet(alphabetStr);

            alphabet.CountAllEntropies(text);
            alphabet.PrintAllData();

            Console.WriteLine("___");
            Console.WriteLine();
        }
    }
}
