using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Core.Enum;
using JmmJ.ToDo.Service.Dto;
using Task = System.Threading.Tasks.Task;

namespace JmmJ.ToDo.Service.Service.IService
{
	public interface ITaskService : IService
	{
		Task<PagedResult<Core.Domain.Task>> GetTasksByTitleAsync(string title, int start, int count, string sortField,OrderBy sortType);
		Task<PagedResult<Core.Domain.Task>> GetTasksAsync(int start, int count, string sortField,OrderBy sortType);
		Task<PagedResult<Core.Domain.Task>> GetTasksByDescriptionAsync(string description, int start, int count, string sortField,OrderBy sortType);
		Task<PagedResult<Core.Domain.Task>> GetTasksByFilter(string param, int start, int count, string sortField, OrderBy sortType);
		Task<PagedResult<Core.Domain.Task>> GetTasksByStatus(Status status, int start, int count, string sortField, OrderBy sortType);
		Task<Result> PutAsync(EditTaskDto taskDto);
		Task<Result> PostAsync(NewTaskDto taskDto);
		Task<Result> DeleteAsync(Guid id);
	}
}
