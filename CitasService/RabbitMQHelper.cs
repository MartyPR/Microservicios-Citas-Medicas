using RabbitMQ.Client;
using System.Text;

public class RabbitMQHelper
{
    public static void SendMessage(string queueName, string message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" }; // Set RabbitMQ server details
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
