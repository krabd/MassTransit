using System.Threading.Tasks;
using MassTransit.Contract;
using MassTransit.Contract.DTO;

namespace MassTransit.Consumer;

public class GetMessageQueryConsumer : IConsumer<GetMessageQuery>
{
    public async Task Consume(ConsumeContext<GetMessageQuery> context)
    {
        await context.RespondAsync(new Result<string>("Body" + context.Message.Id));
    }
}