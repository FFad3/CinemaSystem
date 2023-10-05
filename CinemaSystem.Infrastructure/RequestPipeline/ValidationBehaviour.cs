using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;

namespace CinemaSystem.Infrastructure.RequestPipeline
{
    internal class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validatioResults = await Task.WhenAll(_validators
                .Select(v=> v.ValidateAsync(context, cancellationToken)))
                .ConfigureAwait(false);

            var validationFailures = validatioResults
                .SelectMany(x=>x.Errors)
                .Where(x=>x is not null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

            if (validationFailures.Any())
            {
                throw new FluentValidationException(validationFailures);
            }

            return await next().ConfigureAwait(false);
        }
    }
}