using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Infrastructure.DAL;
using CinemaSystem.Infrastructure.DAL.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>

    {
        private readonly CinemaSystemDbContext _context;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IOptions<SQLOptions> _sqlOptions;
        public TransactionBehaviour(CinemaSystemDbContext context, ILogger<TransactionBehaviour<TRequest, TResponse>> logger, IOptions<SQLOptions> sqlOptions)
        {
            _context = context;
            _logger = logger;
            _sqlOptions = sqlOptions;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //InMem Db doesnt support transactions
            if(_sqlOptions.Value.UseInMemory)
                return await next();

            TResponse result;

            using var dbContextTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _logger.LogDebug("Begin Database Transaction");

                result = await next();
                await _context.SaveChangesAsync(cancellationToken);
                await dbContextTransaction.CommitAsync(cancellationToken);

                _logger.LogDebug("Transaction completed succesfully");
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync(cancellationToken);
                throw;
            }

            return result;
        }
    }
}