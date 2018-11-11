using System;
using System.Net;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using JmmJ.ToDo.Service.Database;
using JmmJ.ToDo.Service.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace JmmJ.ToDo.Api
{
	public class Startup
	{

		public IConfigurationRoot Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }

		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{

			services.AddCors();
			services.AddMvc()
				.AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);
			services.AddDbContext<DatabaseContext>(opt =>
				opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "ToDo API", Version = "v1" });
			});


			var builder = new ContainerBuilder();
			builder.Populate(services);
			builder.RegisterModule(new ContainerModule());
			ApplicationContainer = builder.Build();

			return new AutofacServiceProvider(ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
				});
			}

			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

					var error = context.Features.Get<IExceptionHandlerFeature>();

					if (error != null)
					{
						string errorMsg = JsonConvert.SerializeObject(new { error = error.Error.Message });
						await context.Response.WriteAsync(errorMsg).ConfigureAwait(false);
					}
				});
			});

			var dataInitializer = app.ApplicationServices.GetService<IDatabaseInitializer>();
			dataInitializer.SeedAsync();

			app.UseCors(builder => builder
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());

			app.UseMvc();

			appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
		}
	}
}

