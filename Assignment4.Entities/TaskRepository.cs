using System.Collections.Generic;
using Assignment4.Core;
using static Assignment4.Core.Response;
using System.Linq;
using System;
using System.Data;

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
            var user = context.Users.Find(task.AssignedToId);
            if (user == null) return (BadRequest, -1);
            var tags = new List<Tag>{};
            foreach (var tag in task.Tags)
            {
                var tempTag = new Tag{Name = tag};
                tags.Add(tempTag);
                context.Tags.Add(tempTag);
            }
            var newTask = new Task
            {
                Title = task.Title,
                AssignedTo = user,
                Description = task.Description,
                State = State.New,
                Tags = tags,
                Created = DateTime.UtcNow,
                StatusUpdated = DateTime.UtcNow
            };
            context.Tasks.Add(newTask);
            context.SaveChanges();
            return(Created, newTask.Id);
        }

        public TaskDetailsDTO Read(int taskId)
        {
            var task = context.Tasks.Find(taskId);
            if (task != null)
            {
                return new TaskDetailsDTO
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
            }
            return null;
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            var kc = context;
            var tasks = kc.Tasks.Select(t => t);

            var results = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                results.Add(new TaskDTO(
                    task.Id,
                    task.Title,
                    task.AssignedTo != null ? task.AssignedTo.Name : null,
                    task.Tags == null ? null : task.Tags.Select(t => t.Name).ToList(),
                    task.State
                ));
            }
            return results;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
        {
            var kc = context;
            var tasks = kc.Tasks.Select(t => t);

            var results = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                if (task.State == state)
                {
                    results.Add(new TaskDTO(
                        task.Id,
                        task.Title,
                        task.AssignedTo != null ? task.AssignedTo.Name : null,
                        task.Tags == null ? null : task.Tags.Select(t => t.Name).ToList(),
                        task.State
                    ));
                }
            }
            return results;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
        {
            var kc = context;
            var tasks = kc.Tasks.Select(t => t);

            var results = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                if (task.Tags.Any(t => t.Name.ToLower() == tag.ToLower()))
                {
                    results.Add(new TaskDTO(
                        task.Id,
                        task.Title,
                        task.AssignedTo != null ? task.AssignedTo.Name : null,
                        task.Tags == null ? null : task.Tags.Select(t => t.Name).ToList(),
                        task.State
                    ));
                }
            }
            return results;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
        {
            var kc = context;
            var tasks = kc.Tasks.Select(t => t);

            var results = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                if (task.AssignedTo.Id == userId)
                {
                    results.Add(new TaskDTO(
                        task.Id,
                        task.Title,
                        task.AssignedTo != null ? task.AssignedTo.Name : null,
                        task.Tags == null ? null : task.Tags.Select(t => t.Name).ToList(),
                        task.State
                    ));
                }
            }
            return results;
        }

        public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
        {
            return ReadAllByState(State.Removed);
        }

        public Response Update(TaskUpdateDTO task)
        {
            try
            {
                var oldTask = context.Tasks.Find(task.Id);
                if (oldTask == null) return NotFound;
                
                if(task.AssignedToId != null)
                {
                    var user = context.Users.Find(task.AssignedToId);
                    if (user == null) return BadRequest;
                    else oldTask.AssignedTo = user;
                }
                
                oldTask.Title = task.Title ?? oldTask.Title;
                oldTask.Description = task.Description ?? oldTask.Description;
                oldTask.Tags = task.Tags.Select(t => new Tag{Name = t}).ToList() ?? oldTask.Tags;
                
                if (oldTask.State != task.State)
                {
                    oldTask.StatusUpdated = DateTime.UtcNow;
                    oldTask.State = task.State;
                }

                context.Tasks.Update(oldTask);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return Conflict;
            }
            return Updated;
        }

        public Response Delete(int taskId)
        {
            var task = context.Tasks.Find(taskId);
            if (task == null) return NotFound;

            if (task.State == State.New)
            {
                context.Tasks.Remove(task);
                context.SaveChanges();
                return Deleted;
            }
            else if (task.State == State.Active)
            {
                task.State = State.Removed;
                context.Tasks.Update(task);
                context.SaveChanges();
                return Updated;
            }
            else
            {
                return Conflict;
            }
        }
    }
}
