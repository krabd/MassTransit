using System.Threading;
using System.Threading.Tasks;
using MassTransit.Core.Interfaces;

namespace MassTransit.Core.Services;

public class ProducerService : IProducerService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ProducerService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task ProduceAsync<T>(T message, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}