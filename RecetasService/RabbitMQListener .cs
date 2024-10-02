using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class RabbitMQListener
{
    public static void StartListening(string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

 
                CreateRecetaFromMessage(message);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Waiting for messages...");
            Console.ReadLine();
        }
    }

    private static void CreateRecetaFromMessage(string message)
    {

        var receta = new Receta
        {
            CitaId = Guid.NewGuid().ToString(),
            Paciente = "John Doe",  
            Descripcion = "Prescription based on completed appointment",
            Estado = "Activa",
            FechaCreacion = DateTime.Now
        };

        using (var context = new RecetasDbContext())
        {
            context.Recetas.Add(receta);
            context.SaveChanges();
        }
    }
}
