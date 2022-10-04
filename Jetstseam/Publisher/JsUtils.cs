using NATS.Client.JetStream;

namespace Publisher;

public static class JsUtils
{
    public static StreamInfo CreateStream(IJetStreamManagement jsm, string streamName, StorageType storageType, params string[] subjects) {
        // Create a stream, here will use a file storage type, and one subject,
        // the passed subject.
        StreamConfiguration sc = StreamConfiguration.Builder()
            .WithName(streamName)
            .WithStorageType(storageType)
            .WithSubjects(subjects)
            .Build();

        // Add or use an existing stream.
        StreamInfo si = jsm.AddStream(sc);
        Console.WriteLine("Created stream '{0}' with subject(s) [{1}]\n", streamName, string.Join(",", si.Config.Subjects));
        return si;
    }
    
    public static StreamInfo CreateStream(IJetStreamManagement jsm, string stream, params string[] subjects) {
        return CreateStream(jsm, stream, StorageType.Memory, subjects);
    }
}