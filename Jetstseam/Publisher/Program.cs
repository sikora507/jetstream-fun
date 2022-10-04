using System.Text;
using NATS.Client;
using NATS.Client.JetStream;
using Publisher;

var cf = new ConnectionFactory();
var c = cf.CreateConnection();

const string streamName = "jsfun-stream";
const string streamSubject = "jsfun.subject";

Console.WriteLine("Connection established");

// Create a JetStreamManagement context.
IJetStreamManagement jsm = c.CreateJetStreamManagementContext();

// Use the utility to create a stream stored in memory.
JsUtils.CreateStream(jsm, streamName, streamSubject);

// get a regular context
IJetStream js = c.CreateJetStreamContext();

var options = PublishOptions.Builder()
    .WithExpectedStream(streamName)
    .Build();
for (int i = 0; i < 10; i++)
{
    PublishAck pa = js.Publish(streamSubject, Encoding.ASCII.GetBytes($"message-{i}"), options);
}
