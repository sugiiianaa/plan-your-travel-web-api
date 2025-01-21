using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Users.Commands.CreateUser;
using PlanYourTravel.Application.Users.Commands.LoginUser;
using PlanYourTravel.WebApi.Helper;
using PlanYourTravel.WebApi.Models.Request;
using PlanYourTravel.WebApi.Models.Response;

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

            if (result.IsFailure)
            {
                return BadRequest(ResponseFormatterHelper<CreateUserResponse?>.FormatFailedResponse(result.Error!.ErrorCode));
            }

            var response = new CreateUserResponse
            {
                UserId = result.Value
            };

            return Ok(ResponseFormatterHelper<CreateUserResponse>.FormatSuccessResponse(response));
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

            if (result.IsFailure)
            {
                return BadRequest(ResponseFormatterHelper<LoginUserResponse?>.FormatFailedResponse(result.Error!.ErrorCode));
            }
            var response = new LoginUserResponse
            {
                Token = result.Value
            };

            return Ok(ResponseFormatterHelper<LoginUserResponse>.FormatSuccessResponse(response));
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