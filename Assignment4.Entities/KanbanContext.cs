using Assignment4.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace Assignment4.Entities
{
    public class KanbanContext : DbContext
    {
         protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Data Source=LocalHost;location=BDSA_Assignment4;User ID=sa;password=Kaffe123;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.State)
                .HasConversion(new EnumToStringConverter<State>());
        }
    }
}
