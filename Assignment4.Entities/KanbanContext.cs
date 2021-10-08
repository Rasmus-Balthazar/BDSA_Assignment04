using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Assignment4.Entities
{
    public class KanbanContext : DbContext
    {
        public DbSet<Tag> Tags { get; set;}
        public DbSet<Task> Tasks { get; set;}
        public DbSet<User> Users { get; set;}

        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }
         protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Username=postgres;Password=Kaffe123;Database=BDSA_Assignment4");
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder
                .Entity<Tag>()
                .HasIndex(t => t.tasks)
                .IsUnique();

            modelBuilder
                .Entity<Task>()
                .Property(e => e.State)
                .HasConversion(new EnumToStringConverter<State>());
        }
    }
}
