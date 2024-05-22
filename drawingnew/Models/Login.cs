using System;
using System.Collections.Generic;

namespace drawingnew.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool IsAdmin { get; set; }
}
