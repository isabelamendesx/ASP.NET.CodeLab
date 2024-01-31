using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Data.Dtos;

public class CreateUserDto
{
    [Required] public string Username { get; set; }
    [Required] [DataType(DataType.Password)] public string Password { get; set; }
    [Required] [Compare("Password")] public string RePassword { get; set;}
    [Required] public DateTime BirthData { get; set; }


}
