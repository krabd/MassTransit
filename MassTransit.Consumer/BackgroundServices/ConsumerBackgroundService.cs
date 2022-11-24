using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MassTransit.Consumer.BackgroundServices;

public class ConsumerBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
    }
}