using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace EmployeesWebNetCore
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
            var version = Configuration.GetSection("Version").Value;
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Version")))
            {
                version = Environment.GetEnvironmentVariable("Version");
            }
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Volume Example ", Version = version});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            var version = Configuration.GetSection("Version").Value;
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Version")))
            {
                version = Environment.GetEnvironmentVariable("Version");
            }
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", version); });

            app.UseMvc();
        }
    }
}
