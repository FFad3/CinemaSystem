using CinemaSystem.Application.Abstraction.Common.Auth;
using CinemaSystem.Application.Abstraction.Requests;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.CreateRole
{
    public sealed record CreateRoleCommand(string RoleName) : ICommand<Unit>, ISecret;
}