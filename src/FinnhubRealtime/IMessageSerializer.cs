using FinnhubRealtime.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FinnhubRealtime
{
    internal interface IMessageSerializer
    {
        public byte[] Serialize<TMsg>(TMsg msg) where TMsg : Message;
        public Message Deserialize(ArraySegment<byte> payload);
    }
}
