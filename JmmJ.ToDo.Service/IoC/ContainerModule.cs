using Autofac;
using JmmJ.ToDo.Service.IoC.Modules;

namespace JmmJ.ToDo.Service.IoC
{
	public class ContainerModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterInstance(Mapper.AutoMapper.Initialize()).SingleInstance();
			builder.RegisterModule<RepositoryModule>();
			builder.RegisterModule<ServiceModule>();

		}
	}
}
