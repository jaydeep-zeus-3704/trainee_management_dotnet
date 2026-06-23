namespace trainee_management.Services;

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
public class RabbitMQPublisher:IRabbitMQPublisher
{

    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    public RabbitMQPublisher(IConnection connection,IConfiguration configuration)
    {
        _connection=connection;
        _configuration=configuration;
    }
    public async Task PublishMessageAsync<T>(T message)
    {
         IChannel channel = await _connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(
                queue: _configuration["RabbitMQ:QueueName"]!,
                durable: true,       // Queue survives broker restarts
                exclusive: false,    // Accessible by other connections
                autoDelete: false,   // Do not delete when consumers disconnect
                arguments: null
         );
        string jsonString = JsonSerializer.Serialize(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonString);
        var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent // Message survives broker restarts
            };
         await channel.BasicPublishAsync(
                exchange: string.Empty, // Default exchange routes directly to queues using the routing key
                routingKey: _configuration["RabbitMQ:QueueName"]!,
                mandatory: true,
                basicProperties: properties,
                body: body
            );
        Console.WriteLine($"[x] Sent: {jsonString}");

    }
}
