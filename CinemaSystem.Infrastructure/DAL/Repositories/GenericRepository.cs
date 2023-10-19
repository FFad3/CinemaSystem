using System.Linq.Expressions;
using CinemaSystem.Application.Abstraction.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Infrastructure.DAL.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(CinemaSystemDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<bool> IsUnique(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) =>
            !(await _dbSet.AnyAsync(predicate, cancellationToken));
    }
}