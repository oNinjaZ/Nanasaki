using System;

namespace Nanasaki.Models;

public class User
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    
}