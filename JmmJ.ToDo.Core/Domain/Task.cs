using System;
using JmmJ.ToDo.Core.Enum;


namespace JmmJ.ToDo.Core.Domain
{
	public class Task
	{
		public Guid Id { get; protected set; }
		public string Title { get; protected set; }
		public string Description { get; protected set; }
		public DateTime ExpectedEndDate { get; protected set; }
		public Status Status { get; protected set; }
		public Priority Priority { get; protected set; }
		public DateTime CreatedAt { get; protected set; }


		public static Task Create(string title, string description, DateTime expectedEndDate, Status status, Priority priority) 
			=> new Task(title, description, expectedEndDate, status, priority);


		private Task(string title, string description, DateTime expectedEndDate, Status status, Priority priority)
		{
			Id = Guid.NewGuid();
			SetTitle(title);
			SetDescription(description);
			SetExpectedEndDate(expectedEndDate);
			SetStatus(status);
			SetPriority(priority);
			CreatedAt = DateTime.Now;
		}

		private void SetTitle(string title)
		{
			
			if (string.IsNullOrEmpty(title))
			{
				throw new Exception("Title cannot be empty");
			}
			Title = title;
		}

		private void SetDescription(string description)
		{
			if (string.IsNullOrEmpty(description))
			{
				throw new Exception("Description cannot be empty");
			}
			Description = description;
		}

		private void SetExpectedEndDate(DateTime endDate)
		{
			if (endDate.Millisecond < DateTime.Now.Millisecond)
			{
				throw new Exception("Expected date cannot by lower than current date");
			}
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
