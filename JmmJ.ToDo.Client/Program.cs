using System;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Core.Enum;

namespace JmmJ.ToDo.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Task.Create("Test","test123",DateTime.Now, Status.Ended, Priority.High);
		}
	}
}
