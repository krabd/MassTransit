using MediatR;

namespace MassTransit.Contract.DTO;

public record GetMessageQuery : IRequest<Result<string>>
{
    public long Id { get; set; }
}