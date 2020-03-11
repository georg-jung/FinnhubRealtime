using System;
using System.Collections.Generic;
using System.Text;

namespace FinnhubRealtime.Model
{
    public class TradeMessage : Message
    {
        public TradeMessage() : base("trade")
        {
        }
    }
}
