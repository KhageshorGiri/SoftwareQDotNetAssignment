using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookService.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
}
