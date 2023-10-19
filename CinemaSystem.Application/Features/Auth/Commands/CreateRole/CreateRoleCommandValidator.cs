using CinemaSystem.Application.Abstraction.Infrastructure;
using CinemaSystem.Application.Extensions;
using CinemaSystem.Core.Entities;
using FluentValidation;

namespace CinemaSystem.Application.Features.Auth.Commands.CreateRole
{
    internal sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {

        public CreateRoleCommandValidator(IGenericRepository<Role> roleRepository)
        {
            RuleFor(x => x.RoleName)
               .ValidateRoleName(roleRepository);
        }
    }
}