using System.Threading.Tasks;
using MediatR;

namespace MassTransit.Core.Services;

internal sealed class ConsumerService<T> : IConsumer<T> where T : class
{
    private readonly IMediator _mediator;

    public ConsumerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<T> context)
    {
        return _mediator.Send(context.Message);
    }
}