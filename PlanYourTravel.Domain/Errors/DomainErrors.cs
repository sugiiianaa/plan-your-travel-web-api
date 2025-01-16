using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Email
        {
            public static Error Invalid()
                => new Error("InvalidEmail", "Invalid email address");
        }
    }
}
