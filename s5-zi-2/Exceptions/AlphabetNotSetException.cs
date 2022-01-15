using System;

namespace s5_zi_2.Exceptions 
{ 
    public class AlphabetNotSetException : Exception
    {
        public AlphabetNotSetException() { }
        public AlphabetNotSetException(string message) : base(message) { }
        public AlphabetNotSetException(string message, Exception inner) : base(message, inner) { }
        protected AlphabetNotSetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
