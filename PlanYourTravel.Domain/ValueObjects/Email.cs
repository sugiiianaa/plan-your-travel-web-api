using PlanYourTravel.Domain.Commons.Primitives;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Domain.ValueObjects
{
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
                return Result.Failure<Email>(DomainErrors.Email.Invalid());
            }

            return Result.Success<Email>(new Email(email));
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
            // For equality, we treat sugi@dev.com and SUGI@dev.com as identical
            yield return Value.ToLowerInvariant();
        }
    }
}
