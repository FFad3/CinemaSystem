using CinemaSystem.Application.Abstraction.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CinemaSystem.Application.Abstraction.Requestsr
{
    internal class RequestLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TResponse>, ICommand<TResponse>
    {
        private readonly ILogger<RequestLoggingBehaviour<TRequest, TResponse>> _logger;

        public RequestLoggingBehaviour(ILogger<RequestLoggingBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestAsJson = JsonConvert.SerializeObject(request);
            _logger.LogInformation("Request payload: {requestAsJson}", requestAsJson);
            return await next();
        }
    }
}