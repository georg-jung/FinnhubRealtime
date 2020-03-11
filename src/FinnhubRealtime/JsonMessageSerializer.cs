using FinnhubRealtime.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FinnhubRealtime
{
    internal class JsonMessageSerializer : IMessageSerializer
    {

        private readonly JsonSerializerOptions opts = new JsonSerializerOptions();

        public JsonMessageSerializer()
        {
            opts.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        public Message Deserialize(ArraySegment<byte> payload)
        {
            var str = Encoding.UTF8.GetString(payload);
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException("The given payload contains a null, empty or white space only string. An interpretation as json message is not possible.", nameof(payload));
            if (@"{""type"":""ping""}".Equals(str, StringComparison.Ordinal))
                return PingMessage.Instance;
            if (str.Contains(@"""type"":""error""", StringComparison.Ordinal))
                return JsonSerializer.Deserialize<ErrorMessage>(str, opts); ;
            return JsonSerializer.Deserialize<TradeMessage>(str, opts);
        }

        public byte[] Serialize<TMsg>(TMsg msg) where TMsg : Message => JsonSerializer.SerializeToUtf8Bytes(msg, opts);
    }
}
