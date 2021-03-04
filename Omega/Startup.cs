using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omega.Logic;

namespace Omega
{
    public class Startup
    {
        private OmegaServiceRegistration _omegaServiceRegistration = new OmegaServiceRegistration();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetupDotEnv();
            EnvHelper.Init();
            _omegaServiceRegistration.LoadOmegaServices(services);
            
            services.AddControllers(); // We're letting the react app handle all views, so this is probably all we need.
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _omegaServiceRegistration.ConfigureOmegaServices(app, env);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

        private void SetupDotEnv()
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);
        }
    }
}
