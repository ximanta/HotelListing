using MassTransit;
using RecommendationService.Entities;

namespace RecommendationService.config
{
    public class RabbitConfig : IConsumer<UserProfile>
    {
        public Task Consume(ConsumeContext<UserProfile> context)
        {
            var user = context.Message;
            return Task.CompletedTask;
        }
    }
}
