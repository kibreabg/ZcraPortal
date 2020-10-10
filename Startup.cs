using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ZcraPortal.Data;
using ZcraPortal.Model;

namespace ZcraPortal {

    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();

            services.AddScoped<IZcraPortalRepo, MySqlZcraPortalRepo> ();

            services.AddDbContext<zhcraContext> (opt => opt.UseMySql (
                Configuration.GetConnectionString ("MySqlConnection"),
                optionsBuilder => optionsBuilder.ServerVersion (
                    new Version (10, 1, 26),
                    ServerType.MariaDb)));
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}