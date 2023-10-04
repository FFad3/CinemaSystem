using FluentValidation;
using MediatR;

namespace CinemaSystem.Application.Abstraction.Requests
{
    internal class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>, IAuthCommmand<TResponse>
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

            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .GroupBy(x => new { x.PropertyName, x.ErrorMessage })
                .ToList();

            if (errorsDictionary.Any())
            {
                throw new ValidationException("Tuutaj są błedy jak je wrzucic do srodka");
            }

            return await next().ConfigureAwait(false);
        }
    }
}