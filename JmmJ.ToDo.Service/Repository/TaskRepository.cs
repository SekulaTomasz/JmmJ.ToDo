using System;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.IRepository;
using JmmJ.ToDo.Service.Database;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Core.Enum;

namespace JmmJ.ToDo.Service.Repository
{
	public class TaskRepository : ITaskRepository
	{

		private readonly DatabaseContext _context;

		public TaskRepository(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<Result> Delete(Guid id)
		{
			Result result = new Result();
			try
			{
				var task = await GetTaskById(id);
				if (task == null)
				{
					result.Exception.Add($"Cannot find task with id: {id}");
					result.StatusCode = HttpStatusCode.NotFound;
				}
				if (result.Exception.Any())
				{
					return result;
				}
				result.ResultMessage = "Object removed";
				result.StatusCode = HttpStatusCode.OK;
				_context.Tasks.Remove(task);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				result.Exception.Add(ex.Message);
				result.StatusCode = HttpStatusCode.BadRequest;
			}
			return result;
		}

		public async Task<Core.Domain.Task> GetTaskById(Guid id)
		{
			return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasks(int start, int count, string sortField, OrderBy sortType)
		{
			return await PagedResult<Core.Domain.Task>.CreateAsync(_context.Tasks.AsQueryable(), start, count, sortField, sortType);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksByDescription(string description, int start, int count, string sortField, OrderBy sortType)
		{
			return await PagedResult<Core.Domain.Task>.CreateAsync(_context.Tasks.Where(x => 
				x.Description.ToLowerInvariant().Contains(description.ToLowerInvariant())).AsQueryable(), start, count, sortField, sortType);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksByFilter(string param, int start, int count, string sortField, OrderBy sortType)
		{
			return await PagedResult<Core.Domain.Task>.CreateAsync(_context.Tasks.Where(x =>
				x.Title.ToLowerInvariant().Contains(param.ToLowerInvariant()) || x.Description.ToLowerInvariant().Contains(param.ToLowerInvariant())).AsQueryable(), start, count, sortField, sortType);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksByStatus(Status status, int start, int count, string sortField, OrderBy sortType)
		{
			return await PagedResult<Core.Domain.Task>.CreateAsync(_context.Tasks.Where(x=>x.Status == status).AsQueryable(), start, count, sortField, sortType);
		}

		public async Task<PagedResult<Core.Domain.Task>> GetTasksByTitleAsync(string title, int start, int count, string sortField, OrderBy sortType)
		{
			return await PagedResult<Core.Domain.Task>.CreateAsync(_context.Tasks.Where(x =>
				x.Title.ToLowerInvariant().Contains(title.ToLowerInvariant())).AsQueryable(), start, count, sortField, sortType);
		}

		public async Task<Result> Post(Core.Domain.Task task)
		{
			Result result = new Result();
			try
			{
				if (string.IsNullOrEmpty(task.Title))
				{
					result.Exception.Add("Title cannot be empty");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				if (string.IsNullOrEmpty(task.Description))
				{
					result.Exception.Add("Description cannot be empty");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				if (task.ExpectedEndDate.Ticks < DateTime.UtcNow.Ticks)
				{
					result.Exception.Add("End Date cannot be lower than current date");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				if (result.Exception.Any())
				{
					return result;
				}
				await _context.Tasks.AddAsync(task);
				await _context.SaveChangesAsync();
				result.ResultMessage = "Saved correctly";
				result.StatusCode = HttpStatusCode.Created;
			}
			catch (Exception ex)
			{
				result.Exception.Add(ex.Message);
				result.StatusCode = HttpStatusCode.BadRequest;
			}
			return result;
		}

		public async Task<Result> Put(Core.Domain.Task task)
		{
			Result result = new Result();
			try
			{
				if (task == null)
				{
					result.Exception.Add($"Task cannot be null");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				var oldTask = await GetTaskById(task.Id);
				if (oldTask == null)
				{
					result.Exception.Add($"Cannot find task with id: {task.Id}");
					result.StatusCode = HttpStatusCode.NotFound;
				}
				if (string.IsNullOrEmpty(task.Title))
				{
					result.Exception.Add("Title cannot be empty");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				if (string.IsNullOrEmpty(task.Description))
				{
					result.Exception.Add("Description cannot be empty");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				if (task.ExpectedEndDate.Ticks < DateTime.UtcNow.Ticks)
				{
					result.Exception.Add("End Date cannot be lower than current date");
					result.StatusCode = HttpStatusCode.BadRequest;
				}
				if (result.Exception.Any())
				{
					return result;
				}

				_context.Tasks.Update(task);
				_context.Entry(task).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				result.ResultMessage = "Updated correctly";
				result.StatusCode = HttpStatusCode.OK;
			}
			catch (Exception ex)
			{
				result.Exception.Add(ex.Message);
				result.StatusCode = HttpStatusCode.BadRequest;
			}
			return result;
		}
	}
}

