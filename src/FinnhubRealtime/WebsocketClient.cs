using FinnhubRealtime.Model;
using System;
using System.Globalization;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FinnhubRealtime
{
    public sealed class WebsocketClient : IDisposable
    {
        public const string DefaultEndpointUrl = "wss://ws.finnhub.io?token={0}";
        private readonly ClientWebSocket socket = new ClientWebSocket();
        private readonly Uri endpoint;
        private readonly IMessageSerializer messageSerializer = new JsonMessageSerializer();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054:URI-Parameter dürfen keine Zeichenfolgen sein", Justification = "There is a uri overload.")]
        public WebsocketClient(string token, string endpointUrlFormat = DefaultEndpointUrl)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            if (string.IsNullOrEmpty(endpointUrlFormat))
                throw new ArgumentNullException(nameof(endpointUrlFormat));
            endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, endpointUrlFormat, token));
        }

        public WebsocketClient(Uri endpoint)
        {
            this.endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        }

        public Task Connect(CancellationToken cancellationToken = default) => socket.ConnectAsync(endpoint, cancellationToken);

        public async Task Subscribe(string symbol, CancellationToken cancellationToken = default)
        {
            if (socket.State != WebSocketState.Open)
                throw new InvalidOperationException($"Subscribing is just possible when the socket is open. The socket's state is {socket.State}.");
            var msg = new SubscribeMessage(symbol);
            var msgBytes = messageSerializer.Serialize(msg);
            await socket.SendAsync(msgBytes, WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
        }

        public Task Disconnect(CancellationToken cancellationToken = default) => socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", cancellationToken);

        public async Task<Message> Receive(CancellationToken cancellationToken = default)
        {
            if (socket.State != WebSocketState.Open)
                throw new InvalidOperationException($"Receiving is just possible when the socket is open. The socket's state is {socket.State}.");
            var buffer = WebSocket.CreateClientBuffer(8192, 8192);
            WebSocketReceiveResult res;
            do
            {
                res = await socket.ReceiveAsync(buffer, cancellationToken).ConfigureAwait(false);
            } while (!res.EndOfMessage);
            if (res.MessageType == WebSocketMessageType.Close)
                throw new ConnectionClosedByServerException("The connection was closed by the server.");
            var msg = messageSerializer.Deserialize(buffer);
            return msg;
        }

        public void Dispose()
        {
            if (socket.State == WebSocketState.Open)
                Disconnect().Wait();
            socket.Dispose();
        }
    }
}
