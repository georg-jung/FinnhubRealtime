<p style="text-align: center;">
  <a href="https://www.nuget.org/packages/FinnhubRealtime/">
    <img
      alt="FinnhubRealtime logo"
      src="doc/logo.svg"
      width="100"
    />
  </a>
</p>

# FinnhubRealtime
[![NuGet version (FinnhubRealtime)](https://img.shields.io/nuget/v/FinnhubRealtime.svg?style=flat)](https://www.nuget.org/packages/FinnhubRealtime/)
[![Build Status](https://dev.azure.com/georg-jung/FinnhubRealtime/_apis/build/status/georg-jung.FinnhubRealtime?branchName=master)](https://dev.azure.com/georg-jung/FinnhubRealtime/_build/latest?definitionId=8&branchName=master)

FinnhubRealtime is a .Net Standard 2.1 client library for receiving [Finnhub.io](https://finnhub.io) [real-time price updates](https://finnhub.io/docs/api#websocket-price) via the websocket interface. A small demo application can be found in this repository.

![Screenshot of demo application](doc/screenshot.png)

## Usage

```csharp
cl = new WebsocketClient("YOUR API TOKEN");
await cl.Connect();
await cl.Subscribe("AAPL");
while (true)
{
    var msg = await cl.Receive();
    if (msg is TradeMessage trade) {
        var tradeStr = string.Join("\n", trade.Data.Select(t => $"{t.S} - #{t.V} x {t.P}"));
        Console.WriteLine(tradeStr);
    }
}
```
