using System.Collections.Generic;

namespace JmmJ.ToDo.Service.Helper
{
	public class PagedResult<T>
	{
		public IList<T> Results { get; set; }
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public int PageSize { get; set; }
		public int RowCount { get; set; }
		public string SortDir { get; set; }
		public string SortField { get; set; }

	}
}
