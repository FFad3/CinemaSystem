using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}