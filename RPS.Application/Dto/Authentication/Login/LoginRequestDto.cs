using System.ComponentModel.DataAnnotations;

namespace RPS.Application.Dto.Authentication.Login;

public class LoginRequestDto
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; } = default!;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")] 
    public string Password { get; set; } = default!;
    
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}