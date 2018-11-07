using JmmJ.ToDo.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace JmmJ.ToDo.Service.Database
{
	public class DatabaseContext : DbContext
	{

		public DbSet<Task> Tasks { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var itemBuilder = modelBuilder.Entity<Task>();
			itemBuilder.HasKey(x => x.Id);
		}
	}
}
