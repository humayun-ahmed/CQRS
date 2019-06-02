namespace QueryServer
{
	using Infrastructure.Bootstrapper;
	using Infrastructure.Logger.Contracts;
	using Infrastructure.Logger.Serilog;
	using Infrastructure.Repository;
	using Infrastructure.Repository.Contracts;
	using Infrastructure.SLAdapter.MsDependency;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ApiExplorer;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	using OnlineCourse.Repository;

	using Swashbuckle.AspNetCore.Swagger;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
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
		}

		public void ConfigureServices(IServiceCollection services)
		{
			SeriLogConfiguration.Configure(Configuration.GetSection("logFilePath").Value);

			RegisterDependencies(services);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			/*
			Mapper.Initialize(cfg => {
				cfg.CreateMap<CreateProductCommand, Product>();
				cfg.CreateMap<UpdateProductCommand, Product>();
			});
			*/
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

			services.AddScoped<IRepository, Repository>();
			services.AddScoped<IReadOnlyRepository, Repository>();

			var contextBuilder = new DbContextOptionsBuilder();
			contextBuilder.UseSqlServer(this.Configuration.GetSection("dbConnectionString").Value);
			services.AddSingleton(contextBuilder.Options);
			services.AddScoped<BaseContext, OnlineCourseContext>();

			var adapter = new MsServiceLocatorAdapter(services);
			CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => adapter);
		}
	}
}