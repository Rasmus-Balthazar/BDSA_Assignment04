using System;
using Xunit;
using System.Collections.Generic;
using Assignment4;
using Assignment4.Entities;
using Assignment4.Core;
using Npgsql;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities.Tests
{
    public class TagRepositoryTests : IDisposable
    {
        private readonly IKanbanContext kc;
        private readonly TagRepository tagRepository;
        public TagRepositoryTests()
        {
            using var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            using var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();
            
            this.kc = context;
            tagRepository = new TagRepository(kc);

        }

        public void Create_returns_responseAndId()
        {
            var tag = new TagCreateDTO()
            {
                Name = "WIP"
            };
            var (response, id) = tagRepository.Create(tag);
            var actual = new TagDTO(1, "WIP");
            Assert.Equal(actual.Id, id);
            Assert.Equal(Response.Created, response);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
    

}
