namespace Dummy;

public interface IRequest
{

}

public interface IUseCase<in TRequest, TResponse> : IRequest
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}