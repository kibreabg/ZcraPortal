using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ZcraPortal.Data;
using ZcraPortal.Middlewares;
using ZcraPortal.Model;

namespace ZcraPortal {

    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ().AddNewtonsoftJson (s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
            });

            services.AddScoped<IZcraPortalRepo, MySqlZcraPortalRepo> ();

            services.AddDbContext<zhcraContext> (opt => opt.UseMySql (
                Configuration.GetConnectionString ("MySqlConnection"),
                optionsBuilder => optionsBuilder.ServerVersion (
                    new Version (10, 1, 26),
                    ServerType.MariaDb)));
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());
            services.AddCors (options => {
                options.AddDefaultPolicy (
                    builder => {
                        builder.AllowAnyOrigin ()
                            .AllowAnyHeader ()
                            .AllowAnyMethod ();
                    });
            });

            var jwtSection = Configuration.GetSection ("JWTSettings");
            services.Configure<JWTSettings> (jwtSection);

            //To validate the token which has been sent by clients
            var appSettings = jwtSection.Get<JWTSettings> ();
            var key = Encoding.ASCII.GetBytes (appSettings.SecretKey);

            services.AddAuthentication (x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (x => {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseOptions ();
            app.UseCors ();
            //app.UseHttpsRedirection ();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting ();
            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}