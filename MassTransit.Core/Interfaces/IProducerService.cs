using System.Threading;
using System.Threading.Tasks;

namespace MassTransit.Core.Interfaces;

public interface IProducerService
{
    Task ProduceAsync<T>(T message, CancellationToken cancellationToken);

    Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : class
        where TResponse : class;
}