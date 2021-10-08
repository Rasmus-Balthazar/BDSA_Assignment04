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
        public TaskRepositoryTests()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            using var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();
            this.kc = context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void firstTest()
        {
            var tr = new TaskRepository(kc);

            var t1 = new Task{title = "search"};
            var t2 = new Task{title = "filter"};

            kc.Tasks.Add(t1);
            kc.Tasks.Add(t2);

            kc.SaveChanges();

            var actual = tr.All();
            Assert.Equal(2, actual.Count());
        }
    }
}
