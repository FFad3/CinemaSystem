using System.Linq.Expressions;

namespace CinemaSystem.Application.Abstraction.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> IsUnique(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
