namespace MassTransit.Core.Models.Options;

public class RabbitMqOptions
{
    public string Uri { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}