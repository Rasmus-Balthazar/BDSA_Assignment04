using System.Collections.Generic;
using Assignment4.Core;
using static Assignment4.Core.Response;
using System.Linq;
using System;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IKanbanContext context;
        
        public TaskRepository(IKanbanContext context)
        {
            this.context = context;
        }

        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            User user = context.Users.Find(task.AssignedToId);
            List<Tag> tags = new List<Tag>{};
            foreach (var tag in task.Tags)
            {
                //tags.Add(context.Tags.Find(tag));
                new Tag(){Name = tag, tasks = {}};
            }
            var newTask = new Task(){title = task.Title, AssignedTo = user, Description = task.Description, State = State.New, Tags = tags};

            context.Tasks.Add(newTask);

            context.SaveChanges();

            return (Response.Created, newTask.Id);
        }

        public TaskDetailsDTO Read(int taskId)
        {

            throw new NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            var kc = context;
            var tasks = kc.Tasks.Select( t => t);

            var results = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                results.Add(new TaskDTO(
                    task.Id,
                    task.title,
                    task.AssignedTo != null ? task.AssignedTo.Name : null,
                    task.Tags == null ? null : task.Tags.Select(t => t.Name).ToList(),
                    task.State
                ));
            }
            return results;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            throw new System.NotImplementedException();
        }

        public Response Update(TaskUpdateDTO task)
        {
            throw new System.NotImplementedException();
        }

        Response ITaskRepository.Delete(int taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}
