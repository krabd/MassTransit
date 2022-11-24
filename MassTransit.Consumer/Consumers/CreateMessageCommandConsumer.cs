using System;
using System.Threading.Tasks;
using MassTransit.Contract.DTO;
using MassTransit.Core;
using MediatR;

namespace MassTransit.Consumer.Consumers;

public class CreateMessageCommandConsumer : ConsumerWrapper<CreateMessageCommand>
{
    public CreateMessageCommandConsumer(IMediator mediator) : base(mediator)
    {
    }

    public override Task Consume(ConsumeContext<CreateMessageCommand> context)
    {
        Console.WriteLine(context.Message);

        return base.Consume(context);
    }
}