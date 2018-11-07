using JmmJ.ToDo.Core.IRepository;
using JmmJ.ToDo.Service.Service.IService;

namespace JmmJ.ToDo.Service.Service
{
	public class TaskService : ITaskService
	{
		private readonly ITaskRepository _taskRepository;


		public TaskService(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}
	}
}
