using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FinnhubRealtime
{
    public class ConnectionClosedByServerException : FinnhubException
    {
        public ConnectionClosedByServerException()
        {
        }

        public ConnectionClosedByServerException(string message) : base(message)
        {
        }

        public ConnectionClosedByServerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConnectionClosedByServerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
