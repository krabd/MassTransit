using System.Threading.Tasks;
using MediatR;

namespace MassTransit.Core;

public class ConsumerWrapper<T> : IConsumer<T> where T : class
{
    private readonly IMediator _mediator;

    public ConsumerWrapper(IMediator mediator)
    {
        _mediator = mediator;
    }

    public virtual Task Consume(ConsumeContext<T> context)
    {
        return _mediator.Send(context.Message);
    }
}