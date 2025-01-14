using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Commons.Primitives;

namespace PlanYourTravel.Domain.ValueObjects
{
    [Owned]
    public sealed class Email : ValueObject
    {
        public string Value { get; private set; } = null!;

        private Email() { }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            if (!IsValidEmail(email))
            {
                return Result.Failure<Email>(DomainErrors.User.InvalidEmail);
            }

            return Result.Success(new Email(email));
        }


        private static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLowerInvariant();
        }
    }
}
