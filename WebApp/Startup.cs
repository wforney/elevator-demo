// <copyright file="Startup.cs" company="improvGroup, LLC">
//     Copyright © 2021 improvGroup, LLC. All Rights Reserved.
// </copyright>

namespace WebApp
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.OpenApi.Models;

	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Reflection;

	using WebApp.Data;

	/// <summary>
	/// The startup class.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Startup" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public Startup(IConfiguration configuration) => this.Configuration = configuration;

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>The configuration.</value>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <param name="env">The Web host environment.</param>
		/// <remarks>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </remarks>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				_ = app
					.UseDeveloperExceptionPage()
					.UseSwagger()
					.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1"));
			}

			_ = app
				.UseHttpsRedirection()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => endpoints.MapControllers());
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <remarks>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </remarks>
		[SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "Path is not user input.")]
		public void ConfigureServices(IServiceCollection services) =>
			_ = services
				.AddDbContext<ElevatorDbContext>(options => options.UseSqlite(this.Configuration.GetConnectionString("Sqlite")))
				.AddSwaggerGen(
					c =>
					{
						c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" });

						var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
						var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
						if (File.Exists(filePath))
						{
							c.IncludeXmlComments(filePath);
						}
					})
				.AddControllers();
	}
}
