using System.Collections.Generic;
using System.Text;
using NATS.Client;
using NATS.Client.JetStream;

var cf = new ConnectionFactory();
var c = cf.CreateConnection();

Console.WriteLine("Connection established");

var ctx = c.CreateJetStreamContext();

var options = PullSubscribeOptions.Builder()
    .WithDurable("durable2")
    .Build();

var sub = ctx.PullSubscribe("jsfun.subject", options);

IList<Msg> messages = new List<Msg>();
while (true)
{
    messages = sub.Fetch(1, 1000);
    if (messages.Count > 0)
    {
        var message = messages.Single();
        Console.WriteLine(Encoding.UTF8.GetString(message.Data));
        message.Ack();
        await Task.Delay(100);
        continue;
    }
    await Task.Delay(1000);
}