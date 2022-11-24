using System.Threading;
using System.Threading.Tasks;
using MassTransit.Contract.DTO;
using MassTransit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Producer.Controllers;

/// <summary>
/// Тестирование продюсера
/// </summary>
[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]/[action]")]
public class ProducerController : ControllerBase
{
    private readonly IProducerService _producerService;

    public ProducerController(IProducerService producerService)
    {
        _producerService = producerService;
    }

    [HttpPost]
    public async Task<IActionResult> SendCreateMessage(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await _producerService.ProduceAsync(request, cancellationToken);

        return Ok();
    }
}