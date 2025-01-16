using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Commons
{
    public static class ApplicationError
    {
        public static class User
        {
            public static Error EmailInvalid()
                => new("EmailInvalid", "Email format is invalid");

            public static Error EmailAlreadyRegistered()
                => new("EmailHaveBeenRegistered", "Email already been registered");

            public static Error EmailNotFound()
                => new("EmailNotFound", "Email not found");

            public static Error EmailOrPasswordInvalid()
                => new("EmailOrPasswordInvalid", "Email or password are invalid");
        }
    }
}
