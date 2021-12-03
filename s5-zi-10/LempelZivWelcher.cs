using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_10
{
    public static class LempelZivWelcher
    {
        private static List<CodingTriad> _encodedMessage;
        private static Queue<char?> _buffer;
        private static string _window;
        private static string _message;
        private static int _windowSize;

        public static List<CodingTriad> Encode(string message, int bufferSize, int windowSize)
        {
            InitializeEncodingWelcher(message, bufferSize, windowSize);

            while (_window != null)
            {                
                _encodedMessage.Add(GetNext());

                Console.WriteLine($"Buffer: {string.Join(' ', _buffer)}"); 
                Console.WriteLine($"Window: {string.Join(' ', _window)}");
                Console.WriteLine($"Message: {string.Join(' ', _message)}");
                Console.WriteLine($"Encoded: {string.Join('|', _encodedMessage)}");
                Console.WriteLine();
            }

            return _encodedMessage;
        }

        public static string Decode(List<CodingTriad> encodedMessage, int bufferSize)
        {
            InitializeDecodingWelcher(encodedMessage, bufferSize);

            foreach (var triad in encodedMessage)
                EncodeSingleTriad(triad);

            return _message; 
        }

        private static void InitializeEncodingWelcher(string message, int bufferSize, int windowSize)
        {
            _encodedMessage = new List<CodingTriad>();
            _buffer = new Queue<char?>();
            _window = string.Empty;
            _message = message;
            _windowSize = windowSize;

            while (_buffer.Count < bufferSize)
                _buffer.Enqueue(null);

            FillWindow();
        }

        private static void InitializeDecodingWelcher(List<CodingTriad> encodedMessage, int bufferSize)
        {
            _encodedMessage = encodedMessage;
            _buffer = new Queue<char?>();
            _message = string.Empty;

            while (_buffer.Count < bufferSize)
                _buffer.Enqueue(null);
        }

        private static void FillWindow()
        {
            while (_window != null && _message != null && _window.Length < _windowSize)
            {
                _window = string.Concat(_window, _message[0]);
                _message = _message.RemoveFirstSymbol();
            }
        }

        private static CodingTriad GetNext()
        {
            for (int tempLength = _window.Length; tempLength > 0; tempLength--)
            {
                int startIndex = _buffer.FindIndex(_window.Substring(0, tempLength));

                if (startIndex == -1)
                    continue;

                int clean = tempLength;
                while(clean > 0) // перегоняем повторяющиеся в буфере
                {
                    _buffer.Dequeue();
                    _buffer.Enqueue(_window[0]);
                    _window = _window.RemoveFirstSymbol();

                    clean--;
                }

                char? symbol = GetSingleSymbol();
                return new CodingTriad(startIndex, tempLength, symbol);
            }
            return new CodingTriad(-1, 0, GetSingleSymbol());
        }

        private static char? GetSingleSymbol()
        {
            char? symbol;
            if (_window != null) 
            {
                symbol = _window[0];
                _window = _window.RemoveFirstSymbol();
            }
            else if (_message != null) // если перегнали в буфер все сообщение 
            {
                symbol = _message[0];
                _message = _message.RemoveFirstSymbol();
            }
            else
                symbol = null;

            _buffer.Dequeue();
            _buffer.Enqueue(symbol);

            FillWindow();

            return symbol;
        }

        private static void EncodeSingleTriad(CodingTriad triad)
        {
            if (triad.Position != -1)
            {
                var steps = triad.Amount;
                while (steps > 0)
                {
                    AddSymbolToDecodingMessage(_buffer.ElementAt(triad.Position));
                    steps--;
                }
            }
            AddSymbolToDecodingMessage(triad.Symbol);
        }

        private static void AddSymbolToDecodingMessage(char? symbol)
        {
            _message = string.Concat(_message, symbol);
            _buffer.Enqueue(symbol);
            _buffer.Dequeue();
        }
    }
}
