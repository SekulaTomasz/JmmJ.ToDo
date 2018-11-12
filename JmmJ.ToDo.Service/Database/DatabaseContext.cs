using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Task = JmmJ.ToDo.Core.Domain.Task;

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
		public override int SaveChanges()
		{
			UpdateAuditEntities();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			UpdateAuditEntities();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateAuditEntities();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateAuditEntities();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void UpdateAuditEntities()
		{
			var modifiedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is Task && (x.State == EntityState.Added || x.State == EntityState.Modified));

			var entityEntries = modifiedEntries as IList<EntityEntry> ?? modifiedEntries.ToList();

			foreach (var entry in entityEntries)
			{
				var entity = (Task)entry.Entity;
				DateTime now = DateTime.UtcNow;

				if (entry.State == EntityState.Added)
				{
					entity.Id = Guid.NewGuid();
					entity.CreatedAt = now;
				}
				if (entry.State == EntityState.Modified)
				{
					entity.ModifiedAt = now;
				}

			}
		}
	}
}

