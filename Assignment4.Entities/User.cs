using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class User
    {
        int Id { get; set;}

        [Required]
        [StringLength(100)]
        string Name { get; set;}

        [EmailAddress]
        [Required]
        [StringLength(100)]
        string Email { get; set;}
        List<Task> Tasks { get; set;}
        
    }
}
