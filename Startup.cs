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

namespace ODataSample {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			//services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddOData();
			services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
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
		}

		private static IEdmModel GetEdmModel() {
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Book>("Books").EntityType.Count().Select();
			return builder.GetEdmModel();
		}
	}
}
