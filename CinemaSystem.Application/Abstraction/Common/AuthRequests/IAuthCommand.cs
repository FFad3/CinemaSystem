using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface IAuthCommmand<out TResponse> : IRequest<TResponse>
    {
    }
}