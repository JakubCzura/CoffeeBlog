﻿namespace CoffeeBlog.Domain.SettingsOptions.Email;

public class EmailCoffeeBlogOptions
{
    public string SenderName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}