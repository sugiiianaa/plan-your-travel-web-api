namespace PlanYourTravel.Domain.Shared
{
    /// <summary>
    /// Represents an error with a code and a message.
    /// This can be extended for additional diagnotis info, stack trace, etc.
    /// </summary>
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code} {Message}";
    }
}
