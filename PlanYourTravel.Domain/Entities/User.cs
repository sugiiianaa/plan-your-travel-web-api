using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public sealed class User : Entity
    {
        private User(
            Guid userId,
            string email,
            string password,
            string fullName,
            UserRole role)
            : base(userId)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            Role = role;
        }

        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public UserRole Role { get; private set; }

        public static User Create(
            Guid userId,
            string email,
            string password,
            string fullName,
            UserRole role)
        {
            var user = new User(
                userId,
                email,
                password,
                fullName,
                role);

            return user;
        }
    }
}
