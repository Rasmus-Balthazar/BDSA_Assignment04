using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Assignment4.Entities
{
    public class KanbanContext : DbContext, IKanbanContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder
                .Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder
                .Entity<Task>()
                .Property(e => e.State)
                .HasConversion(new EnumToStringConverter<State>());
            //Data seeding for testing, and seeding proper databases
            /* var rasmus = new User { Id = 1, Name = "Rasmus", Email = "rasm@itu.dk",  Tasks = new List<Task>{}};
            var bente = new User { Id = 2, Name = "Bente", Email = "bent@itu.dk", Tasks = new List<Task>{}};
            var tommy = new User { Id = 3, Name = "Tommy", Email = "tomm", Tasks = new List<Task>{}};
            modelBuilder
                .Entity<User>().HasData(
                    rasmus,
                    bente,
                    tommy
                );

            var cool = new Tag { Id = 1, Name = "Cool", tasks = new List<Task>{}};
            var radical = new Tag { Id = 2, Name = "Radical", tasks = new List<Task>{}};
            var lame = new Tag { Id = 3, Name = "Lame", tasks = new List<Task>{}};
            modelBuilder
                .Entity<Tag>().HasData(
                    cool,
                    radical,
                    lame
                );
            var backflip = new Task { Id = 1, title = "Back Flip", AssignedTo = rasmus, Description = "real cool", State = State.New, Tags = new List<Tag>{cool}};
            var surf = new Task { Id = 2, title = "Surfing was radical and cool!", AssignedTo = bente, Description = "real cool", State = State.Resolved, Tags = new List<Tag>{radical, cool}};
            var bdsa = new Task { Id = 3, title = "BDSA", AssignedTo = rasmus, Description = "awfully lame", State = State.Active, Tags = new List<Tag>{lame}};
            modelBuilder
                .Entity<Task>().HasData(
                    backflip,
                    surf,
                    bdsa
                ); */
        }
    }
}
