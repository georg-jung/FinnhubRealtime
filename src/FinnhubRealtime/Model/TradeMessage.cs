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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Sammlungseigenschaften müssen schreibgeschützt sein", Justification = "Setter used by json deserialization")]
        public List<Trade> Data { get; set; }
    }
}
