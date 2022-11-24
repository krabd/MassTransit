using MassTransit.Contract.DTO;
using MassTransit.Core;
using MediatR;

namespace MassTransit.Consumer.Consumers;

public class UpdateMessageCommandConsumer : ConsumerWrapper<UpdateMessageCommand>
{
    public UpdateMessageCommandConsumer(IMediator mediator) : base(mediator)
    {
    }
}