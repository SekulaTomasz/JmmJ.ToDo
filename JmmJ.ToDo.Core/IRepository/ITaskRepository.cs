using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Domain;
using Task = System.Threading.Tasks.Task;

namespace JmmJ.ToDo.Core.IRepository
{
	public interface ITaskRepository : IRepository
	{
		Task<Domain.Task> GetTaskById(Guid id);
		Task<PagedResult<Domain.Task>> GetTasksByTitleAsync(string title, int start, int count, string sortField);
		Task<PagedResult<Domain.Task>> GetTasks(int start, int count, string sortField);
		Task<PagedResult<Domain.Task>> GetTasksByDescription(string description, int start, int count, string sortField);
		Task<Result> Put(Domain.Task task);
		Task<Result> Post(Domain.Task task);
		Task<Result> Delete(Guid id);
	}
}
