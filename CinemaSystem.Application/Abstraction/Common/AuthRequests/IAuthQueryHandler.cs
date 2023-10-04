using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface IAuthQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IAuthQuery<TResponse>
    { }
}