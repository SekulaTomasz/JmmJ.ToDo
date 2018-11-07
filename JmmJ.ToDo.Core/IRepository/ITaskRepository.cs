using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JmmJ.ToDo.Core.IRepository
{
	public interface ITaskRepository : IRepository
	{
		Task<Task> GetTaskById(Guid id);
		Task<IList<Task>> GetTasksByTitle(string title);
		Task<IList<Task>> GetTasks(int start, int count);
		Task<IList<Task>> GetTasksByDescription(string description);
		Task<Task> Put(Task task);
		Task<Task> Post(Task task);
		Task Delete(Guid id);
	}
}
