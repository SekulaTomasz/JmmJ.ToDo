using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Enum;
using Microsoft.EntityFrameworkCore;
using JmmJ.ToDo.Core;
using JmmJ.ToDo.Service.Service.IService;

namespace JmmJ.ToDo.Service.Database
{

	public interface IDatabaseInitializer : IService
	{
		Task SeedAsync();
	}

	public class DatabaseInitialize : IDatabaseInitializer
	{
		private readonly DatabaseContext _context;

		public DatabaseInitialize(DatabaseContext context)
		{
			_context = context;
		}

		public async Task SeedAsync()
		{
			await _context.Database.MigrateAsync().ConfigureAwait(false);

			if (!await _context.Tasks.AnyAsync())
			{
				await _context.Tasks.AddAsync(Core.Domain.Task.Create("Test1", "test1", DateTime.Now.AddHours(1), Status.Ended, Priority.Medium));
				await _context.Tasks.AddAsync(Core.Domain.Task.Create("Test2", "test2", DateTime.Now.AddHours(1), Status.NotNeded, Priority.High));
				await _context.Tasks.AddAsync(Core.Domain.Task.Create("Test3", "test3", DateTime.Now.AddHours(1), Status.NotNeded, Priority.Low));
				await _context.Tasks.AddAsync(Core.Domain.Task.Create("Test4", "test4", DateTime.Now.AddHours(1), Status.Ended, Priority.Low));
				await _context.Tasks.AddAsync(Core.Domain.Task.Create("Test5", "test5", DateTime.Now.AddHours(1), Status.NotNeded, Priority.High));
				await _context.Tasks.AddAsync(Core.Domain.Task.Create("Test6", "test6", DateTime.Now.AddHours(1), Status.Ended, Priority.Medium));

				await _context.SaveChangesAsync();
			}
		}
	}
}
