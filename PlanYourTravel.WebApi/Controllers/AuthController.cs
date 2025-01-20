using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Users.Commands.CreateUser;
using PlanYourTravel.Application.Users.Commands.LoginUser;
using PlanYourTravel.WebApi.Models;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(
                request.Email,
                request.Password,
                request.FullName);

            var result = await _mediator.Send(command, cancellationToken);

            // TODO : add more proper error
            if (result.IsFailure)
            {
                return BadRequest(result.Error!);
            }

            return Ok(new { Message = "user created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(
                request.Email,
                request.Password);

            var result = await _mediator.Send(command, cancellationToken);

            // TODO : add more proper error
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(new { Token = result.Value });
        }

        // GET /api/auth/user
        [Authorize] // any valid JWT
        [HttpGet("user")]
        public IActionResult UserSecret()
        {
            return Ok("Hello, authenticated user!");
        }

        // GET /api/auth/admin
        [Authorize(Roles = "Admin")] // must have Role = Admin
        [HttpGet("admin")]
        public IActionResult AdminSecret()
        {
            return Ok("Hello, Admin!");
        }
    }
}