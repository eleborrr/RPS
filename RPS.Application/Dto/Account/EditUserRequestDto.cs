using System.ComponentModel.DataAnnotations;

namespace RPS.Application.Dto.Account;

public class EditUserRequestDto
{
    [Required]
    [Display(Name = "Nickname")]
    public string UserName { get; set; } = default!;

    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = default!;
}
