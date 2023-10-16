using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Core.Entities;
using CinemaSystem.Core.Repositories.Auth;
using CinemaSystem.Core.ValueObjects.Common;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.CreateRole
{
    internal sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Unit>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await _roleRepository.GetByNameAsync(request.RoleName, cancellationToken);

            var newRole = new Role(EntityId.Generate(), request.RoleName);

            await _roleRepository.CreateAsync(newRole, cancellationToken);

            return Unit.Value;
        }
    }
}