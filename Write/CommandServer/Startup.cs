namespace CommandServer
{
	using System;
	using System.IO;
	using System.Reflection;

	using CoolBrains.Bus.Contracts;
	using CoolBrains.Bus.ServiceBus.Bus;
	using CoolBrains.Bus.ServiceBusHost.HostingServices;
	using CoolBrains.ServiceBusHost.RabbitMq;
	using CoolBrains.ServiceBusHost.RabbitMq.Extensions;

	using Domain.CommandHandlers;
	using Domain.Commands;

	using GreenPipes;

	using Infrastructure.Bootstrapper;
	using Infrastructure.Logger.Contracts;
	using Infrastructure.Logger.Serilog;
	using Infrastructure.Repository;
	using Infrastructure.Repository.Contracts;
	using Infrastructure.SLAdapter.MsDependency;

	using MassTransit;
	using MassTransit.ExtensionsDependencyInjectionIntegration;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ApiExplorer;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using OnlineCourse.Repository.Domain;
	using OnlineCourse.Repository.Entity;

	using Swashbuckle.AspNetCore.Swagger;

	using Validators;
	using Infrastructure.Validator.Contract;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			WebApiBootstrapper.Use(app);

			var context = CommonServiceLocator.ServiceLocator.Current.GetInstance<BaseContext>();
			context.Database.EnsureCreatedAsync();

			app.UseSwagger();
			app.UseSwaggerUI(
				options =>
					{
						foreach (var description in provider.ApiVersionDescriptions)
						{
							options.SwaggerEndpoint(
								$"/swagger/{description.GroupName}/swagger.json",
								description.GroupName.ToUpperInvariant());
						}
					});
			
			HostBus(app);
		}

		private static void HostBus(IApplicationBuilder app)
		{
			ServiceBusHostProvider.Get().Host(app.ApplicationServices).UseRabbitMq()
				.ListenOn(
					"Domain.Commands",
					e =>
						{
							e.LoadFrom(app.ApplicationServices);
							e.PrefetchCount = 2;
							e.UseConcurrencyLimit(1);
						})
				.UseRetry(2, 2)
				.Start();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			
			services.Configure<RabbitConfig>(this.Configuration.GetSection("RabbitConfig"))
				.AddSingleton<IInmemoryBus, CoolInmemoryBus>()
				.AddSingleton<IMassTransitBus, RabbitMqMassTransitBus>()
				.AddSingleton<IExternalBus, CoolMassTransitBus>()
				.AddSingleton<IExternalBusService, MassTransitRabbitMqHostingService>()
				.AddSingleton<MassTransitRabbitMqHostingService>()
				.AddSingleton<ICoolBus, CoolBus>();
			

			SeriLogConfiguration.Configure(this.Configuration.GetSection("logFilePath").Value);

			RegisterDependencies(services);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			 services.AddMassTransit(cfg =>
			   {
			   cfg.AddConsumer<AddCourseCommandHandler>();
			   cfg.AddConsumer<SignupCourseCommandHandler>();
			   
			   });
			AddVersioningAndDoc(services);
			
		}

		private void AddVersioningAndDoc(IServiceCollection services)
		{
			services.AddVersionedApiExplorer(
				options =>
					{
						options.GroupNameFormat = "'v'VVV";
						options.SubstituteApiVersionInUrl = true;
					});

			services.AddApiVersioning(
				o =>
					{
						o.AssumeDefaultVersionWhenUnspecified = true;
						o.DefaultApiVersion = new ApiVersion(1, 0);
						o.ReportApiVersions = true;
					});

			services.AddSwaggerGen(
				options =>
					{
						var provider = services.BuildServiceProvider()
							.GetRequiredService<IApiVersionDescriptionProvider>();

						foreach (var description in provider.ApiVersionDescriptions)
						{
							options.SwaggerDoc(
								description.GroupName,
								new Info()
									{
										Title = $"Sample API {description.ApiVersion}",
										Version = description.ApiVersion.ToString()
									});
						}
					});
		}

		private void RegisterDependencies(IServiceCollection services)
		{
			services.AddSingleton<ILog, LogUsingSerilog>();

			services.AddScoped<IValidator<AddCourseCommand>, AddCourseCommandValidator>();
			services.AddScoped<IRepository, Repository>();
			services.AddScoped<IReadOnlyRepository, Repository>();
			services.AddScoped<ICourse, Course>();
			

			var contextBuilder = new DbContextOptionsBuilder();
			contextBuilder.UseSqlServer(this.Configuration.GetSection("dbConnectionString").Value);
			services.AddSingleton(contextBuilder.Options);
			services.AddScoped<BaseContext, OnlineCourseDomainContext>();

			var adapter = new MsServiceLocatorAdapter(services);
			CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => adapter);
		}

		public static IConfiguration BuildConfiguration()
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return new ConfigurationBuilder()
				.SetBasePath(path)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		
	}
}