using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    { }
}