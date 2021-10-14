using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using Assignment4.Core;
using Assignment4.Entities;
using System.Linq;

namespace Assignment4.Entities.Tests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly KanbanContext kc;
        private readonly UserRepository ur;
        public UserRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();

            var u1 = new User {Name = "Adam", Email = "a@a.dk"};
            var u2 = new User {Name = "Bob", Email = "b@b.dk"};
            var u3 = new User {Name = "Clara", Email = "c@c.dk"};
            var t1 = new Task {Title = "Back Flip", AssignedTo = u1, Description = "real cool", State = State.New};
            

            context.Users.AddRange(u1, u2, u3);
            context.Tasks.Add(t1);
            context.SaveChanges();
            kc = context;
            ur = new UserRepository(kc);
        }

        [Fact]
        public void Create_given_UserCreateDTO_return_Created_and_UserID()
        {
            var actual = ur.Create(new UserCreateDTO{Name = "Dennis", Email = "deni@itu.dk"});
            var expected = (Response.Created, 4);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Create_given_Adam_return_Conflict_and_minus1()
        {
            var actual = ur.Create(new UserCreateDTO{Name = "Adam", Email = "a@a.dk"});
            var expected = (Response.Conflict, -1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_given_Id_3_return_Deleted()
        {
            var actual = ur.Delete(3);
            var expected = Response.Deleted;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_given_Id_1_return_Conflict()
        {
            var actual = ur.Delete(1);
            var expected = Response.Conflict;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Read_given_2_return_User_Bob()
        {
            var actual = ur.Read(2);
            var user = kc.Users.Find(2);
            var expected = new UserDTO 
            (
                user.Id,
                user.Name,
                user.Email
            );
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadAll_returns_all()
        {
            var actual = ur.ReadAll();
            var expected = new List<UserDTO>();
            foreach (User user in kc.Users)
            {
                expected.Add(new UserDTO 
                (
                    user.Id,
                    user.Name,
                    user.Email
                ));
                
            }
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Updated_given_new_Adam_returns_Updated()
        {
            var actual = ur.Update(new UserUpdateDTO{Id = 1, Email = "a@a.com"});
            var expected = Response.Updated;
            Assert.Equal(expected, actual);
        }

        public void Dispose()
        {
            kc.Database.CloseConnection();
            kc.Dispose();
        }
    }
}
