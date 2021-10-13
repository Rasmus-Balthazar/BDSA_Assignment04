using System;
using Xunit;
using System.Collections.Generic;
using Assignment4.Core;
using Npgsql;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities.Tests
{
    public class TaskRepositoryTests : IDisposable
    {
        private readonly KanbanContext kc;
        private readonly TaskRepository tr;
        public TaskRepositoryTests()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();
            

            var u1 = new User {Name = "Adam", Email = "a@a.dk"};
            var u2 = new User {Name = "Bob", Email = "b@b.dk"};
            var u3 = new User {Name = "Clara", Email = "c@c.dk"};

            var cool = new Tag { Id = 1, Name = "Cool", tasks = new List<Task>{}};
            var radical = new Tag { Id = 2, Name = "Radical", tasks = new List<Task>{}};
            var lame = new Tag { Id = 3, Name = "Lame", tasks = new List<Task>{}};

            var t1 = new Task { Id = 1, title = "Back Flip", AssignedTo = u1, Description = "real cool", State = State.New, Tags = new List<Tag>{cool}};
            var t2 = new Task { Id = 2, title = "Surf!", AssignedTo = u2, Description = "real cool", State = State.Resolved, Tags = new List<Tag>{radical, cool}};
            var t3 = new Task { Id = 3, title = "BDSA", AssignedTo = u1, Description = "awfully lame", State = State.Active, Tags = new List<Tag>{lame}};
            
            context.Users.AddRange(u1, u2, u3);
            context.Tags.AddRange(cool, radical, lame);
            context.Tasks.AddRange(t1, t2, t3);
            context.SaveChanges();

            kc = context;
            tr = new TaskRepository(kc);
        }

        [Fact]
        public void Create_returns_responseAndId()
        {
        //Given
        List<string> tags = new List<string>{"Radical"};
        TaskCreateDTO taskCreateDTO = new TaskCreateDTO(){Title = "eat", AssignedToId = 3, Description = "remember to eat fool!", Tags = tags};
        
        //When
        var created = tr.Create(taskCreateDTO);
        //Then
        Assert.Equal(Response.Created, created.Response);
        Assert.Equal(3, created.TaskId);
        }

        public void Dispose()
        {
            kc.Dispose();
        }

        // [Fact]
        // public void firstTest()
        // {
        //     var tr = new TaskRepository(kc);

        //     var t1 = new Task{title = "search"};
        //     var t2 = new Task{title = "filter"};

        //     kc.Tasks.Add(t1);
        //     kc.Tasks.Add(t2);

        //     kc.SaveChanges();

        //     var actual = tr.ReadAll();
        //     Assert.Equal(2, actual.Count());
        // }
    }
}
