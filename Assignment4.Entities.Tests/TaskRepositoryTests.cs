using System;
using Xunit;
using System.Collections.Generic;
using Assignment4.Core;
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

            var cool = new Tag {Name = "Cool", Tasks = new List<Task>{}};
            var radical = new Tag {Name = "Radical", Tasks = new List<Task>{}};
            var lame = new Tag {Name = "Lame", Tasks = new List<Task>{}};

            var t1 = new Task {Title = "Back Flip", AssignedTo = u1, Description = "real cool", State = State.New, Tags = new List<Tag>{cool}, Created = DateTime.UtcNow, StatusUpdated = DateTime.UtcNow};
            var t2 = new Task {Title = "Surf!", AssignedTo = u2, Description = "real cool", State = State.Resolved, Tags = new List<Tag>{radical, cool}, Created = DateTime.UtcNow, StatusUpdated = DateTime.UtcNow};
            var t3 = new Task {Title = "BDSA", AssignedTo = u1, Description = "awfully lame", State = State.Active, Tags = new List<Tag>{lame}, Created = DateTime.UtcNow, StatusUpdated = DateTime.UtcNow};
            var t4 = new Task {Title = "Program", AssignedTo = u3, Description = "chill work", State = State.Removed, Tags = new List<Tag>{radical}, Created = DateTime.UtcNow, StatusUpdated = DateTime.UtcNow};
            
            //context.Users.AddRange(u1, u2, u3);
            //context.Tags.AddRange(cool, radical, lame);
            context.Tasks.AddRange(t1, t2, t3, t4);
            context.SaveChanges();
            kc = context;
            tr = new TaskRepository(kc);
        }

        [Fact]
        public void Create_given_TaskCreateDTO_return_Created_And_Taskid()
        {
            //Given
            var tags = new List<string>{"Epic"};
            var tempTask = new TaskCreateDTO{Title = "eat", AssignedToId = 3, Description = "remember to eat fool!"};
            
            //When
            var created = tr.Create(tempTask);
            //Then
            Assert.Equal(Response.Created, created.Response);
            Assert.Equal(5, created.TaskId);
        }

        [Fact]
        public void Read_given_2_return_Task_Surf()
        {
            var actual = tr.Read(2);
            var task = kc.Tasks.Find(2);
            var expected = new TaskDetailsDTO 
            (
                task.Id,
                task.Title,
                task.Description,
                DateTime.Now,
                task.AssignedTo.Name,
                task.Tags.Select(t => t.Name).ToList(),
                task.State,
                DateTime.Now
            );
            Assert.Equal(expected.Title, actual.Title);
        }

        [Fact]
        public void ReadAll_returns_all()
        {
            var actual = tr.ReadAll();
            var expected = new List<TaskDTO>();
            foreach (var task in kc.Tasks)
            {
                expected.Add(new TaskDTO 
                (
                    task.Id,
                    task.Title,
                    task.AssignedTo.Name,
                    task.Tags.Select(t => t.Name).ToList(),
                    task.State
                ));
            }
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAllByState_given_Active_returns_BDSA()
        {
            var actual = tr.ReadAllByState(State.Active);
            var expected = new List<TaskDTO>();
            foreach (var task in kc.Tasks)
            {
                if (task.State == State.Active)
                {
                    expected.Add(new TaskDTO 
                    (
                        task.Id,
                        task.Title,
                        task.AssignedTo.Name,
                        task.Tags.Select(t => t.Name).ToList(),
                        task.State
                    ));
                }
            }
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAllByTag_given_cool_returns_2()
        {
            Assert.Equal(2, tr.ReadAllByTag("Cool").Count());
        }

        [Fact]
        public void ReadAllByUser_given_Adam_returns_2()
        {
            Assert.Equal(2, tr.ReadAllByUser(1).Count());
        }

        [Fact]
        public void ReadAllRemoved_returns_1()
        {
            Assert.Equal(1, tr.ReadAllRemoved().Count());
        }

        [Fact]
        public void Update_given_TaskUpdateDTO_return_Updated()
        {
            var expected = tr.Update(new TaskUpdateDTO{Id = 1, State = State.Active});
            var actual = Response.Updated;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_given_1_return_Deleted()
        {
            Assert.Equal(Response.Deleted, tr.Delete(1));
        }


        public void Dispose()
        {
            kc.Database.CloseConnection();
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
