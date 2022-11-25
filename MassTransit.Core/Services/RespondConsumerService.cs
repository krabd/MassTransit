using System.Threading.Tasks;
using MediatR;

namespace MassTransit.Core.Services;

internal sealed class RespondConsumerService<T> : IConsumer<T> where T : class
{
    private readonly IMediator _mediator;

    public RespondConsumerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<T> context)
    {
        var result = await _mediator.Send(context.Message);
        await context.RespondAsync(result);
    }
}