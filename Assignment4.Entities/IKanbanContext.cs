using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public interface IKanbanContext
    {
        DbSet<Tag> Tags { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }
    }
}