using System;
using System.Collections.Generic;
using System.Text;

namespace FinnhubRealtime.Model
{
    internal class SubscribeMessage : Message
    {
        public SubscribeMessage(string symbol) : base("subscribe")
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        }

        public string Symbol { get; }
    }
}
