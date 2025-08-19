﻿namespace ECommerceDemo.Application.Configurations;

public sealed class JwtSettings
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public int ExpiryMinutes { get; set; }
}