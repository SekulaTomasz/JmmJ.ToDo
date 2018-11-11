using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using JmmJ.ToDo.Core.Domain;
using JmmJ.ToDo.Service.Dto;

namespace JmmJ.ToDo.Service.Mapper
{
	public static class AutoMapper
	{
		public static IMapper Initialize() => new MapperConfiguration(config =>
		{
			config.CreateMap<Task, EditTaskDto>();
			config.CreateMap<EditTaskDto, Task>();
			config.CreateMap<Task, NewTaskDto>();
			config.CreateMap<NewTaskDto, Task>()
			.ForMember(x=>x.Id,opt=>opt.Ignore())
			.ForMember(x => x.Id, opt => opt.Ignore());
		}).CreateMapper();
	}
}
