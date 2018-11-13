using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Domain;
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

		
		public async Task<IActionResult> Index(string filter)
		{
			var results = await _taskService.GetTasksAsync(1, 10, "CreatedAt", OrderBy.Desc);
			if (!string.IsNullOrEmpty(filter)) {
				results = await _taskService.GetTasksByFilter(filter, 1, 10, "CreatedAt", OrderBy.Desc);
			}
			
			return View(results);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm]NewTaskDto taskDto)
		{
			if (!ModelState.IsValid)
			{
				return View("Create");
			}
			var results = await _taskService.PostAsync(taskDto);
			if (results.StatusCode == HttpStatusCode.Created)
			{
				return RedirectToAction("Index");
			}
			ViewBag.Errors = new List<string>();
			ViewBag.Errors = results.Exception;
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			Guid.TryParse(id, out Guid guid);
			var task = await _taskService.GetTaskById(guid);
			if (task == null) return RedirectToAction("Index");
			return View(task);
		}


		[HttpPost]
		public async Task<IActionResult> Edit([FromForm]EditTaskDto taskDto)
		{
			if (!ModelState.IsValid)
			{
				return View("Edit", taskDto.Id);
			}
			var results = await _taskService.PutAsync(taskDto);
			if (results.StatusCode == HttpStatusCode.OK)
			{
				return RedirectToAction("Index");
			}
			ViewBag.Errors = new List<string>();
			ViewBag.Errors = results.Exception;
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			Guid.TryParse(id, out Guid guid);
			//return new JsonResult(await _taskService.DeleteAsync(guid));
			var results = await _taskService.DeleteAsync(guid);
			if (results.StatusCode == HttpStatusCode.OK)
			{
				return RedirectToAction("Index");
			}
			ViewBag.Errors = new List<string>();
			ViewBag.Errors = results.Exception;
			return View();
		}


		//[HttpGet("tasks/filter/{filter}")]
		//public async Task<JsonResult> Index(string filter, int start = 1, int count = 10, string sortField = "CreatedAt", OrderBy sortType = OrderBy.Desc)
		//{
		//	var results = await _taskService.GetTasksByFilter(filter, start, count, sortField, sortType);
		//	return new JsonResult(results);
		//}

		//[HttpGet("tasks/status/{status}")]
		//public async Task<JsonResult> Index(Status status, int start = 1, int count = 10, string sortField = "CreatedAt", OrderBy sortType = OrderBy.Desc)
		//{
		//	var results = await _taskService.GetTasksByStatus(status, start, count, sortField, sortType);
		//	return new JsonResult(results);
		//}
	}
}