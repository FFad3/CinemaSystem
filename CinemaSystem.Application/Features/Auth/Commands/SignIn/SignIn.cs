using CinemaSystem.Application.Abstraction.Common.Auth;
using CinemaSystem.Application.Abstraction.Requests;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.SignIn
{
    public sealed record SignIn(string Username, string Password) : ICommand<Unit>, ISecret;
}