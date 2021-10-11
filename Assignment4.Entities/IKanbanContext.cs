using Microsoft.EntityFrameworkCore;
using System;

namespace Assignment4.Entities
{
    public interface IKanbanContext : IDisposable
    {
        DbSet<Tag> Tags { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}