namespace MassTransit.Contract.DTO;

public record CreateMessageCommand
{
    public long Id { get; set; }

    public string Description { get; set; }
}