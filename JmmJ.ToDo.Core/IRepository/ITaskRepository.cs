using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JmmJ.ToDo.Core.IRepository
{
	public interface ITaskRepository
	{
		Task<Task> GetTaskById(int id);
		Task<IEnumerable<Task>> GetTasksByTitle(string title);
		Task<IEnumerable<Task>> GetTasks(int start, int count);
		Task<Task> Put(Task task);
		Task<Task> Post(Task task);
		Task Delete(int id);
	}
}
