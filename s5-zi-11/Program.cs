using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace s5_zi_11
{
    public class InformationString
    {
        public static StringBuilder Sb { get; set; }
    }

    public class Vuvod
    {
        public char Symvol { get; set; }
        public double Verhni_gran { get; set; }
        public double Nizhni_gran { get; set; }
        public double Kod { get; set; }
        public override string ToString()
        {
            return String.Format("Нижняя граница: {1} | Верхняя граница: {0}", Verhni_gran.ToString(), Nizhni_gran.ToString());
        }
        public string ToStrings()
        {
            return String.Format("Код {0} = {1}", Kod.ToString(), Nizhni_gran.ToString());
        }
    }

    public class ArifmCompriession
    {
        public List<Vuvod> Vuvods { get; set; }
        public Dictionary<char, double> Chastota { get; set; }
        public Vuvod ResultVuvod { get; set; }
        public Vuvod ResultVuvods { get; set; }

        public void Build(string source)
        {
            Vuvods = new List<Vuvod>();
            double inc = 1 / (double)source.Length;
            Chastota = new Dictionary<char, double>();
            for (int i = 0; i < source.Length; i++)
            {
                if (!Chastota.ContainsKey(source[i]))
                {
                    Chastota.Add(source[i], 0);
                }
                Chastota[source[i]] += inc;
            }
            Chastota = Chastota.OrderBy(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            double low = 0;
            foreach (var item in Chastota)
            {
                Vuvods.Add(new Vuvod { Symvol = item.Key, Nizhni_gran = low, Verhni_gran = low + item.Value });
                low += item.Value;
            }
        }

        public double Compress(string source)
        {
            InformationString.Sb = new StringBuilder();
            ResultVuvod = new Vuvod { Symvol = '*', Verhni_gran = 1, Nizhni_gran = 0 };
            foreach (var item in source)
            {
                double oldHigh = ResultVuvod.Verhni_gran;
                double oldLow = ResultVuvod.Nizhni_gran;
                InformationString.Sb.Append(ResultVuvod.ToString()).Append(Environment.NewLine);
                ResultVuvod.Symvol = '*';
                ResultVuvod.Verhni_gran = oldLow + (oldHigh - oldLow) * Vuvods.Find(x => x.Symvol == item).Verhni_gran;
                ResultVuvod.Nizhni_gran = oldLow + (oldHigh - oldLow) * Vuvods.Find(x => x.Symvol == item).Nizhni_gran;
            }
            double res = ResultVuvod.Verhni_gran - ResultVuvod.Nizhni_gran;
            InformationString.Sb.Append(ResultVuvod.ToString()).Append(Environment.NewLine);
            return res;
        }

        public double Compress_s(string source)
        {
            InformationString.Sb = new StringBuilder();
            ResultVuvod = new Vuvod { Symvol = '*', Verhni_gran = 1, Nizhni_gran = 0 };
            foreach (var item in source)
            {
                double oldHigh = ResultVuvod.Verhni_gran;
                double oldLow = ResultVuvod.Nizhni_gran;
                InformationString.Sb.Append(ResultVuvod.ToString()).Append(Environment.NewLine);
                ResultVuvod.Symvol = '*';
                ResultVuvod.Verhni_gran = oldLow + (oldHigh - oldLow) * Vuvods.Find(x => x.Symvol == item).Verhni_gran;
                ResultVuvod.Nizhni_gran = oldLow + (oldHigh - oldLow) * Vuvods.Find(x => x.Symvol == item).Nizhni_gran;
            }
            InformationString.Sb.Append(ResultVuvod.ToString()).Append(Environment.NewLine);
            return ResultVuvod.Nizhni_gran;
        }

        public string Decompress(double compress, int leng, int t)
        {
            StringBuilder sb = new StringBuilder();
            InformationString.Sb = new StringBuilder();
            ResultVuvods = new Vuvod();
            for (int i = 0; i < leng; i++)
            {
                ResultVuvods.Kod = i;
                ResultVuvods.Nizhni_gran = compress;
                InformationString.Sb.Append(ResultVuvods.ToStrings()).Append(Environment.NewLine);
                char symbol = Vuvods.Find(x => Math.Round(compress, 15) >= x.Nizhni_gran && Math.Round(compress, 15) <= x.Verhni_gran).Symvol;
                sb.Append(symbol);
                Vuvod tempNode = Vuvods.Find(x => x.Symvol == symbol);
                compress = (compress - tempNode.Nizhni_gran) / (tempNode.Verhni_gran - tempNode.Nizhni_gran);
            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            double Nizhni, result;
            int k = 0;

            ArifmCompriession arifmzhat = new ArifmCompriession();

            string input = String.Empty;
            Console.WriteLine("Введите строку для арифметического сжатия:");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Не верная строка.");
                return;
            }
            arifmzhat.Build(input);
            Console.WriteLine("Интервалы:");

            foreach (var item in arifmzhat.Vuvods)
            {
                Console.WriteLine($"Символ = {item.Symvol} Нижняя граница = {item.Nizhni_gran} Верхняя граница = {item.Verhni_gran}\n");
            }
            result = arifmzhat.Compress(input);
            Nizhni = arifmzhat.Compress_s(input);
            Console.WriteLine("Вычисления: ");
            Console.WriteLine(InformationString.Sb.ToString());

            for (int i = 0; i < result.ToString(CultureInfo.InvariantCulture).Length; i++)
            {
                if ((int)result <= 1)
                {
                    result = result * 10;
                    k = k + 1;
                }
                else break;
            }

            double nizhni_z = Nizhni * (Math.Pow(10, k));
            nizhni_z = (nizhni_z + 1) / Math.Pow(10, k);

            Console.WriteLine($"Нижняя граница последнего символа: {nizhni_z.ToString()}\n" +
                    "Обратное преобразование: \n" +
                    InformationString.Sb.ToString() +
                    $"Исходное сообщение: {arifmzhat.Decompress(nizhni_z, input.Length, input.Length / 2 + 1)}");
        }
    }

}
