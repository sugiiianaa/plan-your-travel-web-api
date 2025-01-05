using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Users.Commands.CreateUser;
using PlanYourTravel.Application.Users.Commands.LoginUser;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Domain.Shared;
using PlanYourTravel.WebApi.Models;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(
                request.Email,
                request.Password,
                request.FullName);

            Result result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                if (result.Error == DomainErrors.User.DuplicateEmail)
                {
                    return Conflict(new { DomainErrors.User.DuplicateEmail.Message });
                }

                return BadRequest(new { result.Error.Message });
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

            Result<string> result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                if (result.Error == DomainErrors.User.InvalidEmailOrPassword)
                {
                    return Conflict(new { DomainErrors.User.InvalidEmailOrPassword.Message });
                }

                return BadRequest(new { result.Error.Message });
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