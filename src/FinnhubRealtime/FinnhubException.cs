using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FinnhubRealtime
{
    public class FinnhubException : Exception
    {
        public FinnhubException()
        {
        }

        public FinnhubException(string message) : base(message)
        {
        }

        public FinnhubException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FinnhubException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
