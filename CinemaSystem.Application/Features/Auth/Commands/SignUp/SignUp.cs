using CinemaSystem.Application.Abstraction.Common.Auth;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Core.Entities;
using MediatR;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    public sealed record SignUp(string Username, string Password, string FirstName, string LastName, string Email) : ICommand<Unit>, ISecret;
}