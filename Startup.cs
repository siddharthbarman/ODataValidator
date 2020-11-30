using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using ODataSample.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ODataSample.Repositories;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace ODataSample {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.AddOData();
			services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
			services.AddLogging(builder => {
				builder
				.ClearProviders()
				.AddNLog(); // Not setting any log level here as NLog will use it from the nlog.config file				
			});
			
			bool useInMemoryDB = Configuration.GetConfigValue<bool>("EntityFramework:UseInMemoryDB"); 
			EFLogLevel efLogLevel = (EFLogLevel)Enum.Parse(typeof(EFLogLevel), Configuration.GetConfigValue<string>("EntityFramework:EnableLogging", "None")); 
			
			if (useInMemoryDB) {
				services.AddDbContext<BookStoreDB>(
				options => {					
					if (efLogLevel > EFLogLevel.None) {
						options.UseLoggerFactory(_loggerFactory);
					}
					if (efLogLevel == EFLogLevel.High) {
						options.EnableSensitiveDataLogging();
					}
					options.UseInMemoryDatabase("BookStore");
				});
			}
			else {			
				services.AddDbContext<BookStoreDB>( 
					options => {
						if (efLogLevel > EFLogLevel.None) {
							options.UseLoggerFactory(_loggerFactory);
						}
						if (efLogLevel == EFLogLevel.High) {
							options.EnableSensitiveDataLogging();
						}
						options.UseSqlServer(Configuration.GetConnectionString("BookStore")); 					
					}
				);
			}			
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseMvc(routeBuilder => {
				routeBuilder.EnableDependencyInjection();
				routeBuilder.Expand().Select().OrderBy().Filter();				
				routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
			});

			using (var serviceScope = app.ApplicationServices.CreateScope()) {
  				var services = serviceScope.ServiceProvider;  				
				BookStoreDB repo = services.GetService<BookStoreDB>();
				bool useInMemoryDB = Configuration.GetConfigValue<bool>("EntityFramework:UseInMemoryDB"); 
				if (useInMemoryDB) {
					repo.SeedForTest();
				}
			}
		}

		private static IEdmModel GetEdmModel() {
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Book>("Books").EntityType.Select().Page(int.MaxValue, 2);
			return builder.GetEdmModel();
		}

		private static ILoggerFactory _loggerFactory = InitializeLogging();
		
		private static ILoggerFactory InitializeLogging() {
			return LoggerFactory.Create(builder => {            
				builder.ClearProviders();
				builder.AddNLog();
				builder.SetMinimumLevel(LogLevel.Trace);		
			});
		}
	}
}
