using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Azure.ServiceBus;
namespace servicebus_sender
{
    class Program
    {
        
        const string ServiceBusConnectionString = "Endpoint=sb://jimmylab-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=DIqHgIjdAQvgRx5UFZm66C0Y8ztM3wXoViuvxRKQs7M=";
        const string QueueName = "jimmylab-sb1";
        static IQueueClient client;
        public static async Task Main(string[] args)
        {
            client = new QueueClient(ServiceBusConnectionString, QueueName);
            var tasks = new List<Task>();
            for (int i = 0; i < 25000; i++)
            {
                string messageBody = $"Message {i}";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                tasks.Add(client.SendAsync(message));
                Console.WriteLine($"Sending message: {messageBody}");
            }
            await Task.WhenAll(tasks);
        }
    }
}
