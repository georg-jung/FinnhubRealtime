using System;
using System.Collections.Generic;
using System.Text;

namespace FinnhubRealtime.Model
{
    public class Trade
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Last price
        /// </summary>
        public decimal P { get; set; }

        /// <summary>
        /// UNIX milliseconds timestamp
        /// </summary>
        public long T { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public decimal V { get; set; }
    }
}
