using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Tag
    {
        int Id { get; set;}
        
        [Required]
        [StringLength(50)]
        string Name { get; set;}
        Task tasks { get; set;}
    }
}
