using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.IRepository;

namespace JmmJ.ToDo.Service.Repository
{
	public class TaskRepository : ITaskRepository
	{
		public Task Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Task> GetTaskById(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IList<Task>> GetTasks(int start, int count)
		{
			throw new NotImplementedException();
		}

		public Task<IList<Task>> GetTasksByDescription(string description)
		{
			throw new NotImplementedException();
		}

		public Task<IList<Task>> GetTasksByTitle(string title)
		{
			throw new NotImplementedException();
		}

		public Task<Task> Post(Task task)
		{
			throw new NotImplementedException();
		}

		public Task<Task> Put(Task task)
		{
			throw new NotImplementedException();
		}
	}
}
