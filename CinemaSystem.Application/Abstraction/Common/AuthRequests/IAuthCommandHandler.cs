using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface IAuthCommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : IAuthCommmand<TResponse>
    { }
}