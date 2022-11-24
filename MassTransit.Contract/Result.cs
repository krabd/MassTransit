namespace MassTransit.Contract;

public record Result
{
    public static Result Empty => new();
}

public record Result<T>(T Data) : Result;