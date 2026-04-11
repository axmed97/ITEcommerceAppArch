namespace Entities.DTOs.AuthDTOs;

public class RegisterDTO
{
    public string? FirstName { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Phone { get; set; } // 
}
