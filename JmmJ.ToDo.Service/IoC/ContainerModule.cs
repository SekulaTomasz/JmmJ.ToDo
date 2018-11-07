using Autofac;
using JmmJ.ToDo.Service.IoC.Modules;

namespace JmmJ.ToDo.Service.IoC
{
	public class ContainerModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule<RepositoryModule>();
			builder.RegisterModule<ServiceModule>();

		}
	}
}
