using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PlanYourTravel.Domain.Entities.UserAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Infrastructure.Services.GetCurrentUser
{
    public class GetCurrentUser(
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository) : IGetCurrentUser
    {
        IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        IUserRepository _userRepository = userRepository;

        public Guid? UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return null;
                }

                return Guid.Parse(userIdClaim.Value);
            }
        }

        public string? EmailAddress
        {
            get
            {
                var emailClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email);
                return emailClaim?.Value;
            }
        }

        public string? Role
        {
            get
            {
                var roleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role);
                return roleClaim?.Value;
            }
        }

        public async Task<User?> GetUserAsync(CancellationToken cancellationToken)
        {
            if (UserId == null)
            {
                return null;
            }

            var emailResult = Email.Create(EmailAddress);
            return await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);
        }
    }
}
