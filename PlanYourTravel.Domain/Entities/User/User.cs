using PlanYourTravel.Domain.Commons.Primitives;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Domain.Entities.User
{
    public sealed class User : AuditableEntity
    {
        /// <summary>
        /// Ef-friendly constructor (private so domain code can't call it).
        /// Calls the base parameterless constructor.
        /// </summary>
        private User() : base()
        {
            // Ef will set properties after construction
        }

        private User(
            Guid id,
            Email email,
            string password,
            string fullName,
            UserRole role)
            : base(id)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            Role = role;
        }

        public Email Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string FullName { get; private set; } = null!;
        public UserRole Role { get; private set; }

        public static User Create(
            Guid id,
            Email email,
            string password,
            string fullName,
            UserRole role)
        {
            return new User(id, email, password, fullName, role);
        }
    }
}
