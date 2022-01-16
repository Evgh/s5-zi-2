using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace s5_zi_4.Logic
{
    public class HammingMatrix
    {
        private int r;
        private int k;
        private Random _random;
        public List<List<bool>> Instance { get; protected set; }

        public HammingMatrix(int k, int r)
        {
            this.r = r;
            this.k = k;
            Instance = new List<List<bool>>();
            _random = new Random();

            GenerateMatrix();
        }

        #region Private Methods

        private void GenerateMatrix()
        {
            for (int rowsCount = 0; rowsCount < k; rowsCount++)
            {
                for (; ; )
                {
                    var tempColumn = GenerateRandomColumn();

                    if (!CheckColumn(tempColumn))
                    {
                        Instance.Add(tempColumn.ToList());
                        break;
                    }
                }
            }

            for (int i = 0; i < r; i++)
            {
                var tempColumn = new bool[r];
                var columnBits = new BitArray(BitConverter.GetBytes((long)Math.Pow(2, i)));

                for(int j = 0; j < tempColumn.Length; j++)
                {
                    tempColumn[j] = columnBits[j]; 
                }
                Instance.Add(tempColumn.ToList());
            }
        }

        private List<bool> GenerateRandomColumn()
        {
            var tempColumn = new int[r];
            int a = _random.Next(3, (int)Math.Pow(2, r) - 1);

            while (a == 4 || a == 8 || a == 16 || a == 32 || a == 64) 
                a = _random.Next(3, (int)Math.Pow(2, r) - 1);

            var bitArray = new BitArray(BitConverter.GetBytes((long)a));
            return bitArray.Cast<bool>().ToList();
        }

        private bool CheckColumn(List<bool> valueForCheck)
        {
            int countRepit = 0;

            for (int i = 0; i < Instance.Count; i++)
            {
                for (int y = 0; y < Instance[i].Count; y++)
                {
                    if (Instance[i][y] == valueForCheck[y])
                    {
                        countRepit++;
                    }
                }

                if (countRepit == Instance[i].Count) return true;
                else countRepit = 0;
            }
            return false;
        }

        #endregion

        #region Public methods 

        public void Print()
        { 
            for(int i = 0; i < r; i++)
            {
                for(int j = 0; j < k+r; j++)
                    Console.Write((Instance[j][i] ? 1 : 0) + " ");
                Console.WriteLine();
            }        
        }
        #endregion
    }
}
