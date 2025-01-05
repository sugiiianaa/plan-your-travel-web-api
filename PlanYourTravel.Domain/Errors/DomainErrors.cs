using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error DuplicateEmail =
                new("User.DuplicateEmail", "The email is already in use.");

            public static readonly Error InvalidEmail =
                new("Email.Invalid", "The email format is invalid.");

            public static readonly Error InvalidEmailOrPassword =
                new("User.InvalidEmailOrPassword", "The email or password is invalid");
        }
    }
}
