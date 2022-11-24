using MediatR;

namespace MassTransit.Contract.DTO;

public record GetMessageQuery : IRequest<string>
{
    public long Id { get; set; }
}