using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Core.Enum;
using Task = System.Threading.Tasks.Task;

namespace JmmJ.ToDo.Core.IRepository
{
	public interface ITaskRepository : IRepository
	{
		Task<Domain.Task> GetTaskById(Guid id);
		Task<PagedResult<Domain.Task>> GetTasksByTitleAsync(string title, int start, int count, string sortField, OrderBy sortType);
		Task<PagedResult<Domain.Task>> GetTasks(int start, int count, string sortField, OrderBy sortType);
		Task<PagedResult<Domain.Task>> GetTasksByDescription(string description, int start, int count, string sortField, OrderBy sortType);
		Task<PagedResult<Domain.Task>> GetTasksByFilter(string param, int start, int count, string sortField, OrderBy sortType);
		Task<PagedResult<Domain.Task>> GetTasksByStatus(Status status, int start, int count, string sortField, OrderBy sortType);
		Task<Result> Put(Domain.Task task);
		Task<Result> Post(Domain.Task task);
		Task<Result> Delete(Guid id);
	}
}
