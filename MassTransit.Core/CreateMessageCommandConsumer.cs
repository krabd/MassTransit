using System;
using System.Threading.Tasks;
using MassTransit.Contract.DTO;

namespace MassTransit.Core;

public class CreateMessageCommandConsumer : IConsumer<CreateMessageCommand>
{
    public Task Consume(ConsumeContext<CreateMessageCommand> context)
    {
        Console.WriteLine(context.Message);

        return Task.CompletedTask;
    }
}