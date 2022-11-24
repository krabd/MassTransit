namespace MassTransit.Contract;

public record Result
{
    public static Result Empty => new();
}