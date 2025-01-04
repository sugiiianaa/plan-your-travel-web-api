using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Domain.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error DuplicateEmail =
                new("User.DuplicateEmail", "The email is already in use.");
        }
    }
}
