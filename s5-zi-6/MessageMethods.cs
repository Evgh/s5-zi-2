using System;
using System.Collections.Generic;
using CummonLogic;

namespace s5_zi_5
{
    public static class MessageMethods
    {
        private static readonly Lazy<Random> lazyRandom = new Lazy<Random>(() => new Random());

        #region public methods

        public static void CreateError(this bool[] message, int errAmount)
        {
            var errIndexes = new List<int>();

            while(errIndexes.Count < errAmount)
            {
                int buff = lazyRandom.Value.Next(message.Length);
                if (!errIndexes.Contains(buff))
                    errIndexes.Add(buff);
            }

            foreach (var index in errIndexes)
                message[index] = !message[index];
        }


        public static bool[] EncodeMessage(bool[] polynomBinare, bool[] messageBinare, int r)
        {
            var newMessageBinare = new bool[messageBinare.Length + r];

            for (int i = 0; i < newMessageBinare.Length; i++)
                newMessageBinare[i] = (messageBinare.Length > i) ? messageBinare[i] : false;

            int[] message = GetNumView(newMessageBinare);
            int[] polynom = GetNumView(polynomBinare);

            Console.WriteLine("Собщение в виде полинома: ");
            Console.WriteLine(String.Join(',', message));

            List<int> divisionPolynom = PolynomDivision(polynom, message);

            Console.WriteLine("Избыточные символы:");
            foreach (int s in divisionPolynom)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Закодированное сообщение:");
            var encodedMessage = new bool[messageBinare.Length + r];
            for (int i = 0, k = 0; i < encodedMessage.Length; i++)
            {
                if (messageBinare.Length > i)
                {
                    encodedMessage[i] = messageBinare[i];
                }
                else
                {
                    if (messageBinare.Length == i)
                    {
                        Console.Write("| ");
                    }
                    if (divisionPolynom.Count > k && encodedMessage.Length - divisionPolynom[k] - 1 == i)
                    {
                        encodedMessage[i] = true;
                        k++;
                    }
                    else
                    {
                        encodedMessage[i] = false;
                    }
                }
                Console.Write((encodedMessage[i] ? 1 : 0) + " ");
            }
            Console.WriteLine();

            return encodedMessage;
        }

        public static void FindFailsAndFix(this bool[] messageBinareWithFail, bool[] polynomBinare, bool[,] matrix, int r)
        {
            int[] message = GetNumView(messageBinareWithFail);
            int[] polynom = GetNumView(polynomBinare);

            List<int> failPolynom = PolynomDivision(polynom, message);
            if (failPolynom.Count == 0)
                return;

            Console.WriteLine();
            var failsIzb = new bool[r];
            for (int i = 0, k = 0; i < failsIzb.Length; i++)
            {
                if (failPolynom.Count > k && failPolynom[k] == failsIzb.Length - i - 1)
                {
                    failsIzb[i] = true;
                    k++;
                }
                else
                {
                    failsIzb[i] = false;
                }
            }

            bool checkFail = false;
            int rowWithFail = 0;
            for (int i = 0, indexIter = 0; i < messageBinareWithFail.Length; i++, indexIter = 0)
            {
                for (int y = 0; y < r; y++)
                {
                    if (matrix[i, y] == failsIzb[y])
                    {
                        indexIter++;
                    }
                    if (indexIter == r)
                    {
                        rowWithFail = i;
                        checkFail = true;
                        break;
                    }
                }
            }
            int d = messageBinareWithFail.Length - rowWithFail - 1;
            Console.WriteLine("Разряд с ошибкой: " + d);
            if (checkFail == true)
            {
                messageBinareWithFail[rowWithFail] = !messageBinareWithFail[rowWithFail];
                Console.WriteLine("Сообщение с исправленной ошибкой:");
                messageBinareWithFail.PrintAsBynary();
            }
            else
            {
                Console.WriteLine("Ошибки две");
            }
        }

        #endregion

        #region private methods
        private static int[] GetNumView(bool[] binary)
        {
            //var numView = new List<int>();


            //for (int i = 0; i < binary.Length; i++)
            //{
            //    if (binary[i])
            //        numView.Add(binary.Length - i - 1);
            //}

            //return numView.ToArray();

            int weight = 0;
            foreach (var s in binary)
            {
                if (s)
                    weight++;
            }

            int[] newView = new int[weight];
            for (int i = 0, k = 0; i < binary.Length; i++)
            {
                if (binary[i])
                {
                    newView[k] = binary.Length - i - 1;
                    k++;
                }
            }

            return newView;
        }

        private static List<int> PolynomDivision(int[] polynom, int[] message)
        {
            List<int> divisionPolynom = new List<int>(message);
            List<int> polynomGenerating = new List<int>(polynom);
            List<int> tempPolynomGenerating;

            for (int gap = 0; divisionPolynom.Count != 0; gap = 0)
            {
                tempPolynomGenerating = polynomGenerating.GetRange(0, polynomGenerating.Count);
                divisionPolynom.Sort();
                divisionPolynom.Reverse();

                gap = divisionPolynom[0] - tempPolynomGenerating[0];
                if (gap < 0)
                {
                    break;
                }
                else
                {
                    for (int y = 0; y < tempPolynomGenerating.Count; y++)
                        tempPolynomGenerating[y] = tempPolynomGenerating[y] + gap;

                    foreach (int s in tempPolynomGenerating)
                    {
                        if (divisionPolynom.Contains(s))
                            divisionPolynom.Remove(s);
                        else
                            divisionPolynom.Add(s);
                    }
                }
            }

            return divisionPolynom;
        }

        #endregion
    }

}
