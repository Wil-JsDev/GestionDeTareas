using Microsoft.AspNetCore.Identity;
namespace GestionDeTareas.Infrastructure.Identity.Models
{
    public class User : IdentityUser
    {
       public string? FirstName { get; set; }

       public string? LastName { get; set; }

       public DateTime? CreateAt { get; set; } = DateTime.UtcNow;
    }
}
