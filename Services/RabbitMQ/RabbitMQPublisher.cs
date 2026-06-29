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
         string queueName=_configuration["RabbitMQ:QueueName"]!;
        Dictionary<string,object?> queueArguments = new Dictionary<string, object?>
        {
         { "x-dead-letter-exchange", "submission-processing-dlx" },
         { "x-dead-letter-routing-key", queueName }
        };

        await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,       
                exclusive: false,    
                autoDelete: false,   
                arguments: queueArguments
         );

        await channel.ExchangeDeclareAsync(
               "submission-processing-dlx",
               ExchangeType.Direct,
               durable: true);

            await channel.QueueDeclareAsync("submission-processing-dead-letter",
               durable: true,
               exclusive: false,
               autoDelete: false);

            await channel.QueueBindAsync("submission-processing-dead-letter", 
            "submission-processing-dlx", 
            routingKey: queueName);


        string jsonString = JsonSerializer.Serialize(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonString);
        
        


        BasicProperties properties = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent ,
        };




         await channel.BasicPublishAsync(
                exchange: string.Empty, 
                routingKey: _configuration["RabbitMQ:QueueName"]!,
                mandatory: true,
                basicProperties: properties,
                body: body
            );

        Console.WriteLine($"[x] Sent: {jsonString}");

    }
}
