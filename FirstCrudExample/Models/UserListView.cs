using System.ComponentModel.DataAnnotations;

namespace FirstCrudExample.Models;


// Model for Login
public class LoginModel
{
    [Required(ErrorMessage = "User ID is required.")]
    public String LoginId { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string UserPassword { get; set; } = null!;

    public bool RememberMe { get; set; }

}

// Model for Register
public class RegisterModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string UserPassword { get; set; } = null!;

    [Required(ErrorMessage = "Confirm password is required.")]
    [DataType(DataType.Password)]
    [Compare(nameof(UserPassword), ErrorMessage = "Passwords do not match.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string ConfirmPassword { get; set; } = null!;
}

// Model for Change Password
public class ChangePasswordModel
{
    [Required(ErrorMessage = "User ID is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Current password is required.")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; } = null!;

    [Required(ErrorMessage = "New password is required.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Confirm new password is required.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "New passwords do not match.")]
    public string ConfirmNewPassword { get; set; } = null!;
}


