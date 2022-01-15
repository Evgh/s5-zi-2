using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace s5_zi_2.Exceptions
{
    [System.Serializable]
    public class StatisticsIsNotGatheredException : Exception
    {
        public StatisticsIsNotGatheredException() { }
        public StatisticsIsNotGatheredException(string message) : base(message) { }
        public StatisticsIsNotGatheredException(string message, Exception inner) : base(message, inner) { }
        protected StatisticsIsNotGatheredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
