using System.ComponentModel.DataAnnotations;

namespace RPS.Application.Dto.Authentication.Register;

public class RegisterRequestDto
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = default!;
    
    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = default!;
}
