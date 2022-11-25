using System.Threading;
using System.Threading.Tasks;
using MassTransit.Core.Interfaces;

namespace MassTransit.Core.Services;

internal class ProducerService : IProducerService
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IClientFactory _clientFactory;

    public ProducerService(IPublishEndpoint publishEndpoint, IClientFactory clientFactory)
    {
        _publishEndpoint = publishEndpoint;
        _clientFactory = clientFactory;
    }

    public Task ProduceAsync<T>(T message, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : class
        where TResponse : class
    {
        var requestClient = _clientFactory.CreateRequestClient<TRequest>();
        var response = await requestClient.GetResponse<TResponse>(request, cancellationToken);

        return response.Message;
    }
}