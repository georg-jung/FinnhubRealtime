using System;
using System.Collections.Generic;
using System.Text;

namespace FinnhubRealtime.Model
{
    public class PingMessage : Message
    {
        public static PingMessage Instance { get; } = new PingMessage();

        public PingMessage() : base("ping")
        {
        }
    }
}
