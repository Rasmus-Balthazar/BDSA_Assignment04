using System.Collections.Generic;
using Assignment4.Core;
using Npgsql;
using System.Linq;
using System.Collections.ObjectModel;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IKanbanContext context;
        public TaskRepository(IKanbanContext context)
        {
            this.context = context;
        }

        public IReadOnlyCollection<TaskDTO> All()
        {
            var kc = context;
            var tasks = kc.Tasks.Select( t => t);

            var result = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                result.Add(new TaskDTO{
                    Id = task.Id,
                    Title = task.title,
                    AssignedToName = task.AssignedTo != null ? task.AssignedTo.Name : null,
                    Tags = task.Tags == null ? null : task.Tags.Select(t => t.Name).ToList()
                    State = task.State,
                });
            }
            return result;
        }

        public int Create(TaskDTO task)
        {
            using var kc = new KanbanContext();
            new Task {
                Id = task.Id,
                title = task.Title,
                de
            }
        }

        public (Response Response, int TaskId) Create(TaskCreateDTO task)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public TaskDetailsDTO Read(int taskId)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyCollection<TaskDTO> ReadAll()
        {
            throw new System.NotImplementedException();
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

        public void Update(TaskDTO task)
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
