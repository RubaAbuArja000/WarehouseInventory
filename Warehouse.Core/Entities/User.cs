using System.ComponentModel.DataAnnotations;

namespace Warehouse.Core.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;

    [Required]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class CreateUserRequest
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsActive { get; set; }
    public string Password { get; set; } = null!;
}

public class UpdateUserRequest
{
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsActive { get; set; }
}

public class ChangePasswordRequest
{
    public string NewPassword { get; set; } = null!;
}

public class ToggleStatusRequest
{
    public bool IsActive { get; set; }

}