using MediatR;

namespace MassTransit.Contract.DTO;

public record UpdateMessageCommand : IRequest<Result>
{
    public long Id { get; set; }

    public string Description { get; set; }
}