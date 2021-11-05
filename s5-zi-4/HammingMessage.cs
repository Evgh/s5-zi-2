using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_4
{
    public class HammingMessage
    {
        private int k;
        private int r;
        private int n;

        private List<bool> _pureMessage;
        private List<bool> _encodedMessage;

        public readonly HammingMatrix HammingMatrix;

        public HammingMessage(string message)
        {            
            var messageBytes = ASCIIEncoding.ASCII.GetBytes(message);
            _pureMessage = s5_zi_2.Encoder.GetBitsFromBytes(messageBytes).ToList();
            _encodedMessage = new List<bool>();

            k = _pureMessage.Count;
            r = (int)Math.Round(Math.Log(k, 2), 0) + 1;
            n = k + r;

            HammingMatrix = new HammingMatrix(k, r);            
            EncodeMessage();
        }

        #region Private methods

        private List<bool> EncodeMessage()
        {
            foreach (var s in _pureMessage)
                _encodedMessage.Add(s);

            for (var i = 0; i < r; i++)
                _encodedMessage.Add(ModPlus(MessageMultRow(GetRow(i))));

            return _encodedMessage;

            List<bool> GetRow(int rowNumber)
            {
                var row = new List<bool>();

                foreach (var column in HammingMatrix.Instance)
                    row.Add(column[rowNumber]);

                return row;
            }
        }

        private List<bool> MessageMultRow(List<bool> row)
        {
            var buff = new List<bool>();

            for (int i = 0; i < k; i++)
            {
                buff.Add(row[i] & _pureMessage[i]);
            }

            return buff;
        }

        private bool ModPlus(List<bool> modRow)
        {
            var result = modRow[0];

            for (int i = 1; i < modRow.Count; i++)
            {
                result ^= modRow[i];
            }

            return result;
        }

        #endregion

        #region Public methods

        public void PrintMessage()
        {
            foreach (var bit in _pureMessage)
                Console.Write(bit ? 1 : 0);
            Console.WriteLine();
        }

        public void PrintEncodedMessage()
        {
            foreach (var bit in _encodedMessage)
                Console.Write(bit ? 1 : 0);
            Console.WriteLine();
        }
        #endregion
    }
}
