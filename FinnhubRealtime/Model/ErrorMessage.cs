using System;
using System.Collections.Generic;
using System.Text;

namespace FinnhubRealtime.Model
{
    public class ErrorMessage : Message
    {
        public ErrorMessage() : base("error")
        {
        }

        public string Msg { get; set; }
    }
}
