using CinemaSystem.Application.Abstraction.Common.Auth;
using CinemaSystem.Application.Abstraction.Requests;
using CinemaSystem.Core.Entities;

namespace CinemaSystem.Application.Features.Auth.Commands.SignUp
{
    public sealed record SignUp : ICommand<User>, ISecret
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}