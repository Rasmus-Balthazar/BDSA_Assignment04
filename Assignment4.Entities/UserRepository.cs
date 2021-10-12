using System.Collections.Generic;
using Assignment4.Core;
using static Assignment4.Core.Response;
using System.Linq;
using System;

namespace Assignment4.Entities
{
    public class UserRepository : IUserRepository
    {
        private readonly IKanbanContext context;
        public UserRepository(IKanbanContext context)
        {
            this.context = context;
        }
        public (Response Response, int UserId) Create(UserCreateDTO user)
        {
            var person = new User {Name = user.Name, Email = user.Email};
            context.Users.Add(person);
            context.SaveChanges();
            return (Created, person.Id);
        }

        public Response Delete(int userId, bool force = false)
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return(Deleted);
            }
            return(NotFound);
        }

        public UserDTO Read(int userId)
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                return new UserDTO
                (
                    user.Id,
                    user.Name,
                    user.Email
                );
            }
            return null;
        }

        public IReadOnlyCollection<UserDTO> ReadAll()
        {
            var result = new List<UserDTO>();
            foreach (var user in context.Users)
            {
                result.Add(new UserDTO
                (
                    user.Id,
                    user.Name,
                    user.Email
                ));
            }
            return result;
        }

        public Response Update(UserUpdateDTO user)
        {
            try
            {
                var oldUser = context.Users.Find(user.Id);
                oldUser.Name = user.Name ?? oldUser.Name;
                oldUser.Email = user.Email ?? oldUser.Email;
                context.Users.Update(oldUser);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return Conflict;
            }
            return Updated;
        }
    }
}
