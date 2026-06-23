namespace trainee_management.Services;
public interface IRabbitMQPublisher
{
     public Task PublishMessageAsync<T>(T message);
}