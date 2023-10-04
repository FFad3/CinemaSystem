using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface IAuthQuery<out TResponse> : IRequest<TResponse>
    {
    }
}