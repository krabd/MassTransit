using System.Threading;
using System.Threading.Tasks;
using MassTransit.Contract;
using MassTransit.Contract.DTO;
using MediatR;

namespace MassTransit.Consumer.Handlers;

public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, Result<string>>
{
    public Task<Result<string>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Result<string>($"Body {request.Id}"));
    }
}