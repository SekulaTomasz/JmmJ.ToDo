using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JmmJ.ToDo.Core.Enum;
using Microsoft.EntityFrameworkCore;

namespace JmmJ.ToDo.Core.Domain
{
	public class PagedResult<T> where T : class
	{
		public IList<T> Results { get; protected set; }
		public int CurrentPage { get; protected set; }
		public int PageSize { get; protected set; }
		public int RowCount { get; protected set; }
		public string SortField { get; protected set; }

		public PagedResult(IList<T> results, int currentPage, int pageSize, int rowCount, string sortField)
		{
			Results = results;
			CurrentPage = currentPage;
			PageSize = pageSize;
			RowCount = rowCount;
			SortField = sortField;
		}

		public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> source, int currentPage,
			int pageSize, string sortField, OrderBy sortType)
		{
			var count = await source.CountAsync();

			var prop = typeof(Core.Domain.Task).GetProperty(sortField) ?? typeof(Core.Domain.Task).GetProperty("CreatedAt");
			List<T> items = null;
			switch (sortType)
			{
				case (OrderBy.Asc):
					items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderBy(x => prop.GetValue(x, null)).ToListAsync();
					break;
				case (OrderBy.Desc):
					items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(x => prop.GetValue(x, null)).ToListAsync();
					break;
			}
			
			return new PagedResult<T>(items, currentPage, pageSize, count, prop.Name);
		}
	}
}
