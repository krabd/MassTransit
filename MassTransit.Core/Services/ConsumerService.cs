using System.Threading.Tasks;
using MediatR;

namespace MassTransit.Core.Services;

internal class ConsumerService<T> : IConsumer<T> where T : class
{
    private readonly IMediator _mediator;

    public ConsumerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public virtual Task Consume(ConsumeContext<T> context)
    {
        return _mediator.Send(context.Message);
    }
}