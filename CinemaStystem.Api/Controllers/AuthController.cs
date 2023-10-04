using CinemaSystem.Application.Features.Auth.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaStystem.Api.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUp request)
        {
            await _mediator.Send(request);

            return Ok();
        }
    }
}