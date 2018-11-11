using System;
using System.Collections.Generic;
using System.Text;
using JmmJ.ToDo.Core.Enum;

namespace JmmJ.ToDo.Service.Dto
{
	public class EditTaskDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime ExpectedEndDate { get; set; }
		public Status Status { get; set; }
		public Priority Priority { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
