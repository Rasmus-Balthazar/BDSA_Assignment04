using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class Task
    {
        public int Id { get; set;}
        [StringLength(100)]
        public string title { get; set;}
        public User AssignedTo { get; set;}
        public string Description { get; set;}

        [Required] 
        public State State { get; set;}
        public List<Tag> Tags { get; set;}

    }
}
