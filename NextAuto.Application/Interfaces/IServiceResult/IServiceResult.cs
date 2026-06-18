namespace NextAuto.Application.Interfaces.IServiceResult;

public interface IServiceResult<T>
{
    bool IsSuccess { get; }
    T? Value { get; }
    string? ErrorMessage { get; }
    int StatusCode { get; }
}