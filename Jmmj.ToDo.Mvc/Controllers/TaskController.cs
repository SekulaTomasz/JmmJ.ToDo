using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Enum;
using JmmJ.ToDo.Service.Dto;
using JmmJ.ToDo.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Jmmj.ToDo.Mvc.Controllers
{
    public class TaskController : Controller
    {
		private readonly ITaskService _taskService;

		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}


		//public IActionResult Index()
		//{
		// return View();
		//}

		public async Task<IActionResult> Index()
		{
			var results = await _taskService.GetTasksAsync(1, 10, "CreatedAt", OrderBy.Desc);
			return View(results.Results);
		}

		//[HttpGet("tasks/title/{title}")]
		//public async Task<IActionResult> GetTasksByTitle(string title, int start = 1, int count = 10, string sortField = "CreatedAt", OrderBy sortType = OrderBy.Desc)
		//{
		//	var results = await _taskService.GetTasksByTitleAsync(title, start, count, sortField, sortType);
		//	return new JsonResult(results);
		//}
		//[HttpGet("tasks/description/{description}")]
		//public async Task<IActionResult> GetTasksByDescription(string description, int start = 1, int count = 10, string sortField = "CreatedAt", OrderBy sortType = OrderBy.Desc)
		//{
		//	var results = await _taskService.GetTasksByDescriptionAsync(description, start, count, sortField, sortType);
		//	return new JsonResult(results);
		//}

		//[HttpGet("tasks/filter/{filter}")]
		//public async Task<IActionResult> GetTasksByFilter(string filter, int start = 1, int count = 10, string sortField = "CreatedAt", OrderBy sortType = OrderBy.Desc)
		//{
		//	var results = await _taskService.GetTasksByFilter(filter, start, count, sortField, sortType);
		//	return new JsonResult(results);
		//}

		//[HttpGet("tasks/status/{status}")]
		//public async Task<IActionResult> GetTasksByStatus(Status status, int start = 1, int count = 10, string sortField = "CreatedAt", OrderBy sortType = OrderBy.Desc)
		//{
		//	var results = await _taskService.GetTasksByStatus(status, start, count, sortField, sortType);
		//	return new JsonResult(results);
		//}


		//[HttpPost]
		//public async Task<IActionResult> CreateTask([FromBody]NewTaskDto taskDto)
		//{
		//	var results = await _taskService.PostAsync(taskDto);
		//	return new JsonResult(results);
		//}

		//[HttpPut]
		//public async Task<IActionResult> UpdateTask([FromBody]EditTaskDto taskDto)
		//{
		//	var results = await _taskService.PutAsync(taskDto);
		//	return new JsonResult(results);
		//}
		//[HttpDelete]
		//public async Task<IActionResult> DeleteTask(string id)
		//{
		//	Guid.TryParse(id, out Guid guid);
		//	//return new JsonResult(await _taskService.DeleteAsync(guid));
		//	return View(new { Values = await _taskService.DeleteAsync(guid)});
		//}
	}
}