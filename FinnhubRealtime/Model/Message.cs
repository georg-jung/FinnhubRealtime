using System;
using System.Collections.Generic;
using System.Text;

namespace FinnhubRealtime.Model
{
    public abstract class Message
    {
        private readonly string type;

        protected Message(string type)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }

        // thechnically Type is writable for deserialization reasons
        public string Type {
            get => type; 
            set {
                if (!type.Equals(value, StringComparison.Ordinal))
                    throw new InvalidOperationException($"The only valid value for the Type property for this kind of message is '{type}'.");
            } 
        }
    }
}
