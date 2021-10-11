using System.Collections.Generic;
using Assignment4.Core;
using Npgsql;
using System.Linq;
using System.Collections.ObjectModel;
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
            throw new NotImplementedException();
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
