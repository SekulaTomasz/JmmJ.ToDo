using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JmmJ.ToDo.Service.Dto;
using JmmJ.ToDo.Service.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JmmJ.ToDo.Api.Controller
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _taskService;

		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}


		[HttpGet("tasks")]
		public async Task<JsonResult> GetTasks(int start = 1, int count = 10, string sortField = "CreatedAt")
		{
			var results = await _taskService.GetTasksAsync(start, count, sortField);
			return new JsonResult(results);
		}

		[HttpGet("tasks/{title}")]
		public async Task<JsonResult> GetTasksByTitle(string title, int start = 1, int count = 10, string sortField = "CreatedAt")
		{
			var results = await _taskService.GetTasksByTitleAsync(title, start, count, sortField);
			return new JsonResult(results);
		}
		[HttpGet("tasks/{description}")]
		public async Task<JsonResult> GetTasksByDescription(string description, int start = 1, int count = 10, string sortField = "CreatedAt")
		{
			var results = await _taskService.GetTasksByDescriptionAsync(description, start, count, sortField);
			return new JsonResult(results);
		}

		[HttpPost]
		public async Task<JsonResult> CreateTask([FromBody]NewTaskDto taskDto)
		{
			var results = await _taskService.PostAsync(taskDto);
			return new JsonResult(results);
		}

		[HttpPut]
		public async Task<JsonResult> UpdateTask([FromBody]EditTaskDto taskDto)
		{
			var results = await _taskService.PutAsync(taskDto);
			return new JsonResult(results);
		}
		[HttpDelete]
		public async Task<JsonResult> DeleteTask(string id)
		{
			Guid.TryParse(id, out Guid guid);
			return new JsonResult(await _taskService.DeleteAsync(guid));
		}
	}
}