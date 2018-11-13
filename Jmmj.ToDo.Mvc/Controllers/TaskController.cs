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
using X.PagedList;

namespace Jmmj.ToDo.Mvc.Controllers
{
	public class TaskController : Controller
	{
		private readonly ITaskService _taskService;

		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		
		public async Task<IActionResult> Index(string filter, OrderBy sortOrder = OrderBy.Desc, string currentFilter = "CreatedAt", int page = 1)
		{
			ViewBag.SortOrder = sortOrder;
			var results = await _taskService.GetTasksAsync(page, 10, currentFilter, sortOrder);
			if (!string.IsNullOrEmpty(filter)) {
				results = await _taskService.GetTasksByFilter(filter, page, 10, currentFilter, sortOrder);
			}

			var usersAsIPagedList = new StaticPagedList<JmmJ.ToDo.Core.Domain.Task>(results.Results, results.CurrentPage, results.PageSize, results.RowCount);
			ViewBag.OnePageOfUsers = usersAsIPagedList;
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
			return RedirectToAction("Index");
		}
	}
}