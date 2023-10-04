using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CinemaSystem.Application.Abstraction.Requests
{
    internal class ResultLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TResponse>, ICommand<TResponse>
    {
        private readonly ILogger<ResultLoggingBehaviour<TRequest, TResponse>> _logger;

        public ResultLoggingBehaviour(ILogger<ResultLoggingBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();

            var resultAsJson = JsonConvert.SerializeObject(result);

            _logger.LogInformation("Request result: {resultAsJson}", resultAsJson);

            return result;
        }
    }
}