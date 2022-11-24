using MassTransit.Contract.DTO;
using MassTransit.Core;
using MediatR;

namespace MassTransit.Consumer.Consumers;

public class CreateMessageCommandConsumer : ConsumerWrapper<CreateMessageCommand>
{
    public CreateMessageCommandConsumer(IMediator mediator) : base(mediator)
    {
    }
}