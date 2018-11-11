using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Service.Dto;
using Task = System.Threading.Tasks.Task;

namespace JmmJ.ToDo.Service.Service.IService
{
	public interface ITaskService : IService
	{
		Task<PagedResult<Core.Domain.Task>> GetTasksByTitleAsync(string title, int start, int count, string sortField);
		Task<PagedResult<Core.Domain.Task>> GetTasksAsync(int start, int count, string sortField);
		Task<PagedResult<Core.Domain.Task>> GetTasksByDescriptionAsync(string description, int start, int count, string sortField);
		Task<Result> PutAsync(EditTaskDto taskDto);
		Task<Result> PostAsync(NewTaskDto taskDto);
		Task<Result> DeleteAsync(Guid id);
	}
}
