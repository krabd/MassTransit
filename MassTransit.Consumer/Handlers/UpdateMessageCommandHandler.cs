using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Contract;
using MassTransit.Contract.DTO;
using MediatR;

namespace MassTransit.Consumer.Handlers;

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Result>
{
    public Task<Result> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Mediator request = {request}");

        return Task.FromResult(Result.Empty);
    }
}