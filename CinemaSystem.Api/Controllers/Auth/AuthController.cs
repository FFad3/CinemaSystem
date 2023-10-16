using CinemaSystem.Application.Features.Auth.Commands.SignIn;
using CinemaSystem.Application.Features.Auth.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaSystem.Api.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(SignIn request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(SignUp request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken()
        //{
        //    throw new NotImplementedException();
        //}
    }
}