using System;
using CummonLogic;
using s5_zi_2;

namespace s5_zi_3
{
    public class WorkExecutor
    {
        private readonly string _sourcePath;
        private readonly string _value;
        private readonly string _key;

        public WorkExecutor(string sourcePath, string value, string key)
        {
            _sourcePath = sourcePath;
            _value = value;
            _key = key;
        }

        public void ExecuteRoutine()
        {
            Console.WriteLine("Lab №3");

            var info = _sourcePath.ReadAsFileSourceFromProjectRoot();

            AlphabetRoutine("abcdefghijklmnopqrstuvwxyz", info);
            AlphabetRoutine($"{Base64Encoder.Base64Alphabet}{Base64Encoder.FillSymbol}",
                            Base64Encoder.Encode(info));

            XorAsciiRoutine();
            XorBase64Routine();
        }


        private void AlphabetRoutine(string alphabetSource, string baseForStatistics)
        {
            Alphabet alphabet = new Alphabet(alphabetSource);
            AlphabetInformationPrinter printer = new(alphabet);

            alphabet.CalculateAllEntropies(baseForStatistics);
            printer.PrintAlphabetData();

            Console.WriteLine("___");
            Console.WriteLine();
        }

        private void XorAsciiRoutine()
        {
            string singleAsciiXorResult = XOR.ExecuteXorAscii(_value, _key);
            string doubleAsciiXorResult = XOR.ExecuteXorAscii(singleAsciiXorResult, _key);

            Console.WriteLine($"Original value: {_value} \nAscii Xor Results:\n");
            Console.WriteLine(singleAsciiXorResult);
            Console.WriteLine(doubleAsciiXorResult);
            Console.WriteLine();
        }

        private void XorBase64Routine()
        {
            string base64Value = Base64Encoder.Encode(_value);
            string base64Key = Base64Encoder.Encode(_key);
            string singleBase64Xor = XOR.ExecuteXorBase64(base64Value, base64Key);
            string doubleBase64Xor = XOR.ExecuteXorBase64(singleBase64Xor, base64Key);

            Console.WriteLine($"Value encoded in Base64: {base64Value} \nXor Results:\n");
            Console.WriteLine(singleBase64Xor);
            Console.WriteLine(doubleBase64Xor);
            Console.WriteLine();
        }
    }
}
