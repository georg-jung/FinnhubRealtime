using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using FinnhubRealtime.Model;

namespace FinnhubRealtime.Demo
{
    class Program
    {
        private const string ApiToken = null; //--- INSERT YOUR API TOKEN HERE ---
        private static WebsocketClient cl;
        private static readonly string[] symbols = new string[] { "BINANCE:BTCUSDT", "IC MARKETS:1" };
        private static readonly CancellationTokenSource cts = new CancellationTokenSource();
        private static bool cancelPressed = false;

        static async Task Main(string[] args)
        {
            try
            {
                Console.CancelKeyPress += CancelKeyPressed;

                var token = ApiToken;
                while (string.IsNullOrWhiteSpace(token))
                {
                    Console.Write("Please specify your api token: ");
                    token = Console.ReadLine();
                }

                Console.WriteLine("Connecting to Finnhub...");
                cl = new WebsocketClient(token);
                await cl.Connect(cts.Token);
                Console.WriteLine("Connected.");

                foreach (var symbol in symbols)
                {
                    await cl.Subscribe(symbol, cts.Token);
                    Console.WriteLine($"Subscribed {symbol}");
                }

                while (!cancelPressed)
                {
                    var msg = await cl.Receive(cts.Token);
                    switch (msg)
                    {
                        case PingMessage ping:
                            Console.WriteLine("Ping");
                            break;
                        case ErrorMessage err:
                            Console.WriteLine($"ERROR returned by Finnhub: {err.Msg}");
                            break;
                        case TradeMessage trade:
                            var tradeMsg = string.Join("\n", trade.Data.Select(t => $"{t.S} - #{t.V} x {t.P}"));
                            Console.WriteLine($"Trade: {tradeMsg}");
                            break;
                        default:
                            Console.WriteLine($"WARNING: Unknown message received.");
                            break;
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (WebSocketException wse)
            {
                Console.WriteLine($"CONNECTION ERROR: {wse}");
            }
        }

        static void CancelKeyPressed(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            cancelPressed = true;
            Console.WriteLine("Exiting...");
            cts.Cancel();
        }
    }
}
