using CinemaSystem.Application.Abstraction.Infrastructure;
using Humanizer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaSystem.Application.Abstraction.Requests
{
    internal class PereformenceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly ILogger<PereformenceBehaviour<TRequest, TResponse>> _logger;
        private readonly IClock _clock;

        public PereformenceBehaviour(IClock clock, ILogger<PereformenceBehaviour<TRequest, TResponse>> logger)
        {
            this._clock = clock;
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name.Underscore();

            var startTime = _clock.Current();
            _logger.LogInformation("Begin request Name: {requestName}", requestName);

            var result = await next();

            var endTime = _clock.Current();

            var elapsedMiliseconds = (startTime - endTime).TotalMilliseconds;

            _logger.LogInformation("End request Name: {request} | Execution time: {time}", requestName, elapsedMiliseconds);

            return result;
        }
    }
}