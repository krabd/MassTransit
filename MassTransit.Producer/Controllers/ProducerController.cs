using System.Threading;
using System.Threading.Tasks;
using MassTransit.Contract;
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

    [HttpGet]
    public async Task<IActionResult> GetMessage([FromQuery]long id, CancellationToken cancellationToken)
    {
        var body = await _producerService.RequestAsync<GetMessageQuery, Result<string>>(new GetMessageQuery{Id = id}, cancellationToken);

        return Ok(body);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await _producerService.ProduceAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessage(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        await _producerService.ProduceAsync(request, cancellationToken);

        return NoContent();
    }
}