using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Insurance.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Settings
            services.AddOptions<ConnectionStringsSettings>().Bind(Configuration.GetSection("ConnectionStrings"));
            services.AddOptions<ProductAPISettings>().Bind(Configuration.GetSection("ProductAPI"));

            //Repositories
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductTypeRepository, ProductTypeRepository>();
            services.AddSingleton<ISurchargeRepository, SurchargeRepository>();

            //Services
            services.AddSingleton<ISurchargeService, SurchargeService>();
            services.AddSingleton<IInsuranceCalculator, InsuranceCalculator>();

            //Controllers
            services.AddControllers();

            //Logging
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
