﻿namespace PlanYourTravel.Infrastructure.Settings
{
    public sealed class JwtSettings
    {
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
        public int ExpiryMinutes { get; init; }
    }
}
