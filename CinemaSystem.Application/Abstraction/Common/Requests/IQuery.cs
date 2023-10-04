using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}