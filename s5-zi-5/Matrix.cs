using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_5
{
    public class Matrix
    {
        private int k;
        private int r;
        public bool[,] Instance { get; private set; }

        public Matrix(bool[] polynom, int messageK, int messageR)
        {
            k = messageK;
            r = messageR;

            var newPolynomWithR = new bool[k + r];
            Instance = new bool[k + r, k];

            for (int i = 0; i < newPolynomWithR.Length; i++)
                newPolynomWithR[i] = polynom.Length > i ? polynom[i] : false;


            bool[] tempPolynom = newPolynomWithR;
            for (int i = 0; i < k; i++)
            {
                if (i != 0)
                {
                    tempPolynom = RightShift(tempPolynom);
                }
                for (int y = 0; y < tempPolynom.Length; y++)
                {
                    Instance[y, i] = tempPolynom[y];
                    Console.Write((Instance[y, i] ? 1 : 0) + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            RowSum(newPolynomWithR.Length, messageK);
            Console.WriteLine();

            TranspMatrix(r, messageK);
            Console.WriteLine();
        }

        private bool[] RightShift(bool[] polynom)
        {
            var temp = new bool[polynom.Length];

            for (int i = 0, lenth = polynom.Length; i < lenth; i++)
            {
                temp[i] = i != 0 ? polynom[i-1] : polynom[polynom.Length - 1];
            }

            return temp;
        }

        private void RowSum(int rowLenght, int columnLenght)
        {
            for (int indexDiagonal = 0; indexDiagonal < columnLenght; indexDiagonal++)
            {
                for (int i = indexDiagonal + 1; i < columnLenght; i++)
                {
                    if (Instance[i, indexDiagonal])
                    {
                        for (int y = i; y < rowLenght; y++)
                        {
                            if (Instance[y, indexDiagonal] == Instance[y, i])
                            {
                                Instance[y, indexDiagonal] = false;
                            }
                            else
                            {
                                Instance[y, indexDiagonal] = true;
                            }
                        }
                    }
                }

                for (int y = 0; y < rowLenght; y++)
                {
                    Console.Write((Instance[y, indexDiagonal] ? 1 : 0) + " ");
                }
                Console.WriteLine();
            }
        }

        public void TranspMatrix(int r, int lenghtPolynom)
        {
            var tempTransMatrix = new bool[r, lenghtPolynom + r];

            for (int i = 0, diagonalMatrix = 0; i < lenghtPolynom + r; i++)
            {
                if (i < lenghtPolynom)
                {
                    for (int y = lenghtPolynom, columnIndex = 0; y < lenghtPolynom + r; y++, columnIndex++)
                        tempTransMatrix[columnIndex, i] = Instance[y, i];
                }
                else
                {
                    for (int columnIndex = 0; columnIndex < r; columnIndex++)
                        tempTransMatrix[columnIndex, i] = diagonalMatrix == columnIndex;

                    diagonalMatrix++;
                }
            }

            var transMatrix = new bool[lenghtPolynom + r, r];
            for (int i = 0; i < r; i++)
            {
                for (int y = 0; y < lenghtPolynom + r; y++)
                    transMatrix[y, i] = tempTransMatrix[i, y];
            }
            Instance = transMatrix;
        }        
    }
}
