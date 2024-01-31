using Microsoft.AspNetCore.Identity;

namespace UsersAPI.Models;

public class User : IdentityUser
{
    public DateTime BirthDate { get; set; }

    public User() : base ()
    {
        
    }
}
