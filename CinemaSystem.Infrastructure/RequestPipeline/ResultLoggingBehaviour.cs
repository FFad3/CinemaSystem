using CinemaSystem.Application.Abstraction.Common.Auth;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal class ResultLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ResultLoggingBehaviour<TRequest, TResponse>> _logger;

        public ResultLoggingBehaviour(ILogger<ResultLoggingBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();

            if(request is ISecret)
                return result;

            var resultAsJson = JsonConvert.SerializeObject(result);

            _logger.LogInformation("Request result: {resultAsJson}", resultAsJson);

            return result;
        }
    }
}