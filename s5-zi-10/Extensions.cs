using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_10
{
    public static class Extensions
    {
        public static int FindIndex(this Queue<char?> str, string value)
        {
            if (value == null || value.Length > str.Count)
                return -1;

            for (int i = 0; i <= str.Count - value.Length; i++)
            {
                bool flag = true;

                for (int j = i, k = 0; k < value.Length; j++, k++)
                {
                    flag &= str.ElementAt(j).Equals(value[k]);
                }
                if (flag)
                    return i;
            }

            return -1;
        }


        public static string RemoveFirstSymbol(this string str)
        {
            return str.Length > 1 ? str.Substring(1) : null;
        }
    }
}
