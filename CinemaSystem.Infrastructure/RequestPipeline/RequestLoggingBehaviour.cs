using CinemaSystem.Application.Abstraction.Common.Auth;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<RequestLoggingBehaviour<TRequest, TResponse>> _logger;

        public RequestLoggingBehaviour(ILogger<RequestLoggingBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ISecret)
            {
                var requestAsJson = JsonConvert.SerializeObject(request);
                _logger.LogInformation("Request payload: {requestAsJson}", requestAsJson);
            }
            return await next();
        }
    }
}