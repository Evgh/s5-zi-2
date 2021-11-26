using System;
using System.Collections.Generic;

namespace s5_zi_5
{
    public class Source
    {
        public static int[] GenerateMessage(int size)
        {
            int[] _newMessage = new int[size];
            Random rd = new Random();

            _newMessage[0] = 1;
            for (int i = 1; i < size; i++)
            {
                _newMessage[i] = rd.Next(0, 2);
            }

            return _newMessage;
        }

        public static int ModSum(int value1, int value2)
        {
            int result;

            if (value1 == value2)
            {
                result = 0;
            }
            else
            {
                result = 1;
            }
            return result;
        }

        public static int[] CodingMessage(int[] _message, int paratetNum, int k1, int k2, int z)
        {
            List<int> _codingMessage = new List<int>();
            foreach (int s in _message)
            {
                _codingMessage.Add(s);
            }
            //Горизонтальные паритеты
            if (paratetNum >= 1)
            {
                for (int row = 0, tempInt = 0; row < k1; row++, tempInt = 0)
                {
                    for (int column = 0; column < k2; column++)
                    {
                        tempInt = ModSum(tempInt, _message[row * k2 + column]);
                    }
                    _codingMessage.Add(tempInt);
                }
            }
            //Вертикальные паритеты
            if (paratetNum >= 2)
            {
                for (int column = 0, tempInt = 0; column < k2; column++, tempInt = 0)
                {
                    for (int row = 0; row < k1; row++)
                    {
                        tempInt = ModSum(tempInt, _message[row * k2 + column]);
                    }
                    _codingMessage.Add(tempInt);
                }
            }

            //Паритет паритетов
            if (paratetNum >= 3)
            {
                int iter = 0;
                _codingMessage.ForEach(s =>
                {
                    if (s == 1)
                    {
                        iter++;
                    }
                });
                _codingMessage.Add(iter % 2);
            }

            return _codingMessage.ToArray();
        }

        public static int[] DecodingMessage(int[] _codingMessage, int paratetNum, int k1, int k2, int z, int k)
        {
            int[] _decodingMessage = new int[k];
            int[] _tempSourceMessage = new int[_codingMessage.Length - k];
            int[] _tempCopyMessage = new int[_codingMessage.Length - k];

            for (int i = 0; i < k; i++)
            {
                _decodingMessage[i] = _codingMessage[i];
            }
            _decodingMessage = CodingMessage(_decodingMessage, paratetNum, k1, k2, z);

            Console.Write("\tизбыточность принятого: ");
            for (int i = 0, y = k; i < _codingMessage.Length - k; i++, y++)
            {
                _tempSourceMessage[i] = _codingMessage[y];
                Console.Write(_tempSourceMessage[i]);
            }
            Console.WriteLine();

            Console.Write("\tизбыточность повторная: ");
            for (int i = 0, y = k; i < _decodingMessage.Length - k; i++, y++)
            {
                _tempCopyMessage[i] = _decodingMessage[y];
                Console.Write(_tempCopyMessage[i]);
            }
            Console.WriteLine();

            Console.Write("\tвектор ошибок:          ");
            int[] _error = new int[_tempSourceMessage.Length];
            for (int i = 0; i < _tempSourceMessage.Length; i++)
            {
                _error[i] = ModSum(_tempSourceMessage[i], _tempCopyMessage[i]);
                Console.Write(_error[i]);
            }
            Console.WriteLine();

            _decodingMessage = new int[k];
            for (int i = 0; i < k; i++)
            {
                _decodingMessage[i] = _codingMessage[i];
            }

            //int corZ = 1;
            int row = 0;
            int column = 0;
            for (int i = 0; i < _error.Length - 1; i++)
            {
                if (_error[i] == 1 && i < k1 && z == 0)
                {
                    row = i;
                }
                else if (_error[i] == 1 && i < k2 + k1 && i > k1 && z == 0)
                {
                    column = i - k1;
                }
            }
            _decodingMessage[row * k2 + column] = ModSum(1, _decodingMessage[row * k2 + column]);

            return _decodingMessage;
        }

        public static void CreateFail(int index, int[] mas)
        {
            if (mas[index] == 1)
            {
                mas[index] = 0;
            }
            else
            {
                mas[index] = 1;
            }
        }

        static void Main()
        {
            int messageLength = 24;
            int[] _masK1 = { 4, 3, 2, 6 };
            int[] _masK2 = { 6, 8, 3, 2 };
            int[] _masZ = { 0, 0, 4, 2 };
            int[] _masParatet = { 3, 3, 5, 5 };

            int[] _message = GenerateMessage(messageLength);
            Console.WriteLine("Сообщение: ");
            Console.WriteLine(string.Join("", _message));

            Random rd = new Random();

            int indexFail;
            int temp;
            int[] _codingMessage;
            int[] _tempMessage;

            for (int i = 0; i < 4; i++)
            {
                _codingMessage = CodingMessage(_message, _masParatet[i], _masK1[i], _masK2[i], _masZ[i]);
                Console.WriteLine("\n\n:>Кодовое сообщение: ");
                Console.WriteLine(string.Join("", _codingMessage));

                _tempMessage = _codingMessage;

                indexFail = rd.Next(0, messageLength);
                CreateFail(indexFail, _tempMessage);

                Console.WriteLine($"ошибка [1] на позиции {indexFail}:");
                Console.WriteLine(string.Join("", _tempMessage));                
                Console.WriteLine("исправление: ");
                Console.WriteLine(string.Join("", DecodingMessage(_tempMessage, _masParatet[i], _masK1[i], _masK2[i], _masZ[i], messageLength)));


                temp = indexFail;
                while (indexFail == temp)
                {
                    indexFail = rd.Next(0, messageLength);
                }

                CreateFail(indexFail, _tempMessage);
                Console.WriteLine($"ошибки [2] на позициях {indexFail}, {temp}:");
                Console.WriteLine(string.Join("", _tempMessage));

                Console.WriteLine("исправление: ");
                Console.WriteLine(string.Join("", DecodingMessage(_tempMessage, _masParatet[i], _masK1[i], _masK2[i], _masZ[i], messageLength)));
            }
        }
    }
}
