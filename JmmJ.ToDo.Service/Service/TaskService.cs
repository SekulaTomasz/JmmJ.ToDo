using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Core.IRepository;
using JmmJ.ToDo.Service.Dto;
using JmmJ.ToDo.Service.Service.IService;
using Task = System.Threading.Tasks.Task;

namespace JmmJ.ToDo.Service.Service
{
	public class TaskService : ITaskService
	{
		private readonly ITaskRepository _taskRepository;
		private readonly IMapper _mapper;


		public TaskService(ITaskRepository taskRepository, IMapper mapper)
		{
			_taskRepository = taskRepository;
			_mapper = mapper;
		}

		public async Task<Result> DeleteAsync(Guid id)
		{
			return await _taskRepository.Delete(id);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksAsync(int start, int count, string sortField)
		{
			return await _taskRepository.GetTasks(start, count, sortField); ;
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksByDescriptionAsync(string description, int start, int count, string sortField)
		{
			return await _taskRepository.GetTasksByDescription(description, start, count, sortField);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksByTitleAsync(string title, int start, int count, string sortField)
		{
			return await _taskRepository.GetTasksByTitleAsync(title, start, count, sortField);
		}

		public async Task<Result> PostAsync(NewTaskDto taskDto)
		{
			var task = _mapper.Map<NewTaskDto, Core.Domain.Task>(taskDto);
			return await _taskRepository.Post(task);
		}

		public async Task<Result> PutAsync(EditTaskDto taskDto)
		{
			var task = _mapper.Map<EditTaskDto, Core.Domain.Task>(taskDto);
			return await _taskRepository.Put(task);
		}
	}
}



