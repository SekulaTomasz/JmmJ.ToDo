using System;
using JmmJ.ToDo.Core.Enum;


namespace JmmJ.ToDo.Core.Domain
{
	public class Task
	{
		public Guid Id { get; set; }
		public string Title { get; protected set; }
		public string Description { get; protected set; }
		public DateTime ExpectedEndDate { get; protected set; }
		public Status Status { get; protected set; }
		public Priority Priority { get; protected set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }


		public static Task Create(string title, string description, DateTime expectedEndDate, Status status, Priority priority) 
			=> new Task(title, description, expectedEndDate, status, priority);

		private Task(string title, string description, DateTime expectedEndDate, Status status, Priority priority)
		{
			SetTitle(title);
			SetDescription(description);
			SetExpectedEndDate(expectedEndDate);
			SetStatus(status);
			SetPriority(priority);
		}

		private void SetTitle(string title)
		{
			Title = title;
		}

		private void SetDescription(string description)
		{
			Description = description;
		}

		private void SetExpectedEndDate(DateTime endDate)
		{
			ExpectedEndDate = endDate;
		}

		private void SetStatus(Status status)
		{
			Status = status;
		}

		private void SetPriority(Priority priority)
		{
			Priority = priority;
		}

	}
}
