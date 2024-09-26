using RabbitMQ.Client;
using System.Text;

namespace CitasService
{
    public class RabbitMQSender
    {
        public void EnviarMensaje(string mensaje)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cola_recetas", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(mensaje);
                channel.BasicPublish(exchange: "", routingKey: "cola_recetas", basicProperties: null, body: body);
            }
        }
    }
}
