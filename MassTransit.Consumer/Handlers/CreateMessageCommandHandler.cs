using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Contract;
using MassTransit.Contract.DTO;
using MediatR;

namespace MassTransit.Consumer.Handlers;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result>
{
    public Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Mediator request = {request}");

        return Task.FromResult(Result.Empty);
    }
}