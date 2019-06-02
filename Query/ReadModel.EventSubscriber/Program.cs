using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using CoolBrains.Bus.Contracts;
using CoolBrains.Bus.Contracts.Command;
using CoolBrains.Bus.Contracts.Event;
using CoolBrains.Bus.ServiceBus.Bus;
using CoolBrains.Bus.ServiceBusHost.HostingServices;
using CoolBrains.ServiceBusHost.RabbitMq;
using CoolBrains.ServiceBusHost.RabbitMq.Extensions;
using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ReadModel.EventSubscriber
{
	using Domain.Events;

	using Infrastructure.Repository;
	using Infrastructure.Repository.Contracts;

	using Microsoft.EntityFrameworkCore;

	using OnlineCourse.Repository;

	class Program
	{
		public static IConfiguration BuildConfiguration()
		{
			var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return new ConfigurationBuilder()
				.SetBasePath(path)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		private static IServiceProvider ConfigureServices()
		{
			var configuration = BuildConfiguration();
			var services = new ServiceCollection()
				.AddSingleton<IConfiguration>(configuration)
				.Configure<RabbitConfig>(configuration.GetSection("RabbitConfig"))
				.AddSingleton<IInmemoryBus, CoolInmemoryBus>()
				.AddSingleton<IMassTransitBus, RabbitMqMassTransitBus>()
				.AddSingleton<IExternalBus, CoolMassTransitBus>()
				.AddSingleton<IExternalBusService, MassTransitRabbitMqHostingService>()
				.AddSingleton<MassTransitRabbitMqHostingService>()
				.AddScoped<IRepository, Repository>()
				.AddScoped<IReadOnlyRepository, Repository>()
				.AddSingleton<ICoolBus, CoolBus>();

			var contextBuilder = new DbContextOptionsBuilder();
			contextBuilder.UseSqlServer(configuration.GetSection("dbConnectionString").Value);
			services.AddSingleton(contextBuilder.Options);
			services.AddScoped<BaseContext, OnlineCourseContext>();


			services.AddMassTransit(cfg =>
			{
				
				cfg.AddConsumer<CourseCreatedEventHandler>();
				
			});

			return services.BuildServiceProvider();
		}

		static async Task Main(string[] args)
		{
			try
			{
				var serviceProvider = ConfigureServices();
				ServiceBusHostProvider.Get().Host(serviceProvider).UseRabbitMq()
					.ListenOn("Domain.Events",
					e =>
					{
						e.LoadFrom(serviceProvider);
						e.PrefetchCount = 2;
						e.UseConcurrencyLimit(1);
					})
					.UseRetry(2, 2)
					.Start();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			Console.WriteLine("Listening...");
		}
	}
}
