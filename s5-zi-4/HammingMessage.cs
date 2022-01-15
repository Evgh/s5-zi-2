using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLogic;

namespace s5_zi_4
{
    public class HammingMessage
    {
        private int k;
        private int r;
        private int n;
        private Random _random;

        private List<bool> _pureMessage;
        private List<bool> _encodedMessage;

        public readonly HammingMatrix HammingMatrix;

        public HammingMessage(string message)
        {
            _random = new Random();
            _encodedMessage = new List<bool>();

            var messageBytes = ASCIIEncoding.ASCII.GetBytes(message);
            _pureMessage = BinaryEncoder.GetBitsFromBytes(messageBytes).ToList();

            k = _pureMessage.Count;
            r = (int)Math.Round(Math.Log(k, 2), 0) + 1;
            n = k + r;

            HammingMatrix = new HammingMatrix(k, r);            
            EncodeMessage(_pureMessage, out _encodedMessage);
        }

        #region Private methods

        private List<bool> EncodeMessage(List<bool> pure, out List<bool> encoded)
        {
            encoded = new List<bool>(pure);
            for (var i = 0; i < r; i++)
                encoded.Add(ModPlus(MessageMultRow(GetRow(i), pure)));

            return encoded;
        }

        private List<bool> GetRow(int rowNumber)
        {
            var row = new List<bool>();

            foreach (var column in HammingMatrix.Instance)
                row.Add(column[rowNumber]);

            return row;
        }

        private List<bool> MessageMultRow(List<bool> row, List<bool> message)
        {
            var buff = new List<bool>();

            for (int i = 0; i < k; i++)
                buff.Add(row[i] & message[i]);

            return buff;
        }

        private bool ModPlus(List<bool> modRow)
        {
            var result = modRow[0];

            for (int i = 1; i < modRow.Count; i++)
                result ^= modRow[i];

            return result;
        }

        private int CountSyndrome(List<bool> sended, List<bool> encoded)
        {
            if (sended.Count != encoded.Count)
                throw new ArgumentException("messages lenth should be equal");

            int syndrome = 0;
            for(int i = k + r - 1, j = 0; i >= k; --i, j++)
            {
                syndrome += (sended[i]^encoded[i]) ? 1 << j : 0; 
            }
            return syndrome;
        }

        private int GetRepairMask(int syndrome)
        {
          //  var syndromeBytes = s5_zi_2.Encoder.GetBitsFromBytes(BitConverter.GetBytes(syndrome));
            for(int i = 0; i < HammingMatrix.Instance.Count; i++)
            {
                bool flag = true;
                var buffSyndrome = syndrome;

                for (int j = 0; j < r && flag; j++, buffSyndrome >>= 1)
                {
                    flag &= !(((buffSyndrome&1) == 1) ^ HammingMatrix.Instance[i][r - j - 1]);
                }

                if (flag)
                    return i;
            }
            return -1;
        }

        #endregion

        #region Public methods

        public void SendMessageWithErrors(int errAmount)
        {
            var errIndexes = new int[errAmount];
            var sended = new List<bool>(_encodedMessage);

            for(int i = 0; i < errAmount; i++)
                errIndexes[i] = _random.Next(_encodedMessage.Count - 1);
            foreach (var err in errIndexes)
                sended[err] = !sended[err];

            List<bool> pureSended = new List<bool>();
            for (int i = 0; i < k; i++)
                pureSended.Add(sended[i]);

            List<bool> encoded;
            EncodeMessage(pureSended, out encoded);

            var syndrome = CountSyndrome(sended, encoded);
            var mask = GetRepairMask(syndrome);
            var repaired = new List<bool>(sended);
            if(mask >= 0)
                repaired[mask] = !repaired[mask];

            Console.WriteLine("Отправлено сообщение с ошибками на позициях: ");
            foreach (var err in errIndexes)
                Console.Write(err + ", ");
            Console.WriteLine();
            BinaryEncoder.PrintAsBinary(sended.ToArray());

            Console.WriteLine("pure: ");
            BinaryEncoder.PrintAsBinary(pureSended.ToArray());
            Console.WriteLine("encoded: ");
            BinaryEncoder.PrintAsBinary(encoded.ToArray());

            Console.WriteLine("Синдром: ");
            Console.WriteLine(syndrome);
            Console.WriteLine("Ошибка в бите сообщения номер: ");
            Console.WriteLine(mask);
            Console.WriteLine("Сообщение исправлено: ");
            BinaryEncoder.PrintAsBinary(repaired.ToArray());
        }

        public void PrintMessage() => BinaryEncoder.PrintAsBinary(_pureMessage.ToArray());
        public void PrintEncodedMessage() => BinaryEncoder.PrintAsBinary(_encodedMessage.ToArray());

        #endregion
    }
}
