using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Filters;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Tools;

namespace WebApiRest.NetCore
{
    public class Startup
    {
        public IConfiguration _Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Default configuration.
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(l =>
                {
                    l.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // Add swagger.
            services
                .AddSwaggerGen(l =>
                {
                    // Add swagger doc.
                    l.SwaggerDoc(
                        "v1",
                        new Info()
                        {
                            Version = "v1",
                            Description = "Web api the type Proof of concept.",
                            Title = "WebApiRest.NetCore",
                        }
                    );
                    //Add security definition.
                    l.AddSecurityDefinition(
                        "Bearer",
                        new ApiKeyScheme
                        {
                            Description = "Please enter jwt with Bearer into field.",
                            In = "header",
                            Name = "Authorization",
                            Type = "apikey"
                        }
                    );
                    // Add security requiement.
                    l.AddSecurityRequirement(
                        new Dictionary<string, IEnumerable<string>> {
                            { "Bearer", Enumerable.Empty<string>() },
                        }
                    );
                });

            // Set JWT authentification.
            services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      // What to valide
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateIssuerSigningKey = true,
                      // Setup validate data
                      ValidIssuer = "projects.in",
                      ValidAudience = "readers",
                      IssuerSigningKey = Methods.GetSymmetricSecurityKey(
                                            this._Configuration.GetSection("AppSettings:SecurityKey").Value
                                         )
                  };
              });

            // Set context
            services
              .AddDbContext<DataBaseSQLServerContext>(options =>
              {
                  options.UseSqlServer(
                        _Configuration.GetConnectionString("DataBaseSQLServerContextConnectionString")
                    );
              })
              .AddDbContext<DataBaseMySQLContext>(options =>
              {
                  options.UseMySQL(
                        _Configuration.GetConnectionString("TestDBMySqlEntities")
                    );
              });

            // Add AutoMapper.
            services
                .AddAutoMapper();

            // Add independency injection
            services
              .AddScoped<IAuthorizationRepository, WebApirest.NetCore.Repositories.SQLServer.AuthorizationRepositoryImpl>()
              .AddScoped<IUserRepository, WebApirest.NetCore.Repositories.SQLServer.UserRepositoryImpl>()
              .AddScoped<IMenuRepository, WebApirest.NetCore.Repositories.SQLServer.MenuRepositoryImpl>()
              .AddScoped<IRoleRepository, WebApirest.NetCore.Repositories.SQLServer.RoleRepositoryImpl>()
              .AddScoped<IGroupMenuRepository, WebApirest.NetCore.Repositories.SQLServer.GroupMenuRepositoryImpl>();

            // Add independency injection
            services
              .AddScoped<IAuthorizationBussiness, WebApirest.NetCore.Bussiness.SQLServer.AuthorizationBussinessImpl>()
              .AddScoped<IUserBussiness, WebApirest.NetCore.Bussiness.SQLServer.UserBussinessImpl>()
              .AddScoped<IMenuBussiness, WebApirest.NetCore.Bussiness.SQLServer.MenuBussinessImpl>()
              .AddScoped<IRoleBussiness, WebApirest.NetCore.Bussiness.SQLServer.RoleBussinessImpl>()
              .AddScoped<IGroupMenuBussiness, WebApirest.NetCore.Bussiness.SQLServer.GroupMenuBussinessImpl>();

            // Add filter in scope.
            services
                .AddScoped<CustomActionFilter>();

            // Config IIS
            services
              .Configure<IISOptions>(opt =>
              {
                  opt.AutomaticAuthentication = false;
                  opt.ForwardClientCertificate = false;
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Custom error message.
            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "application/json";

                context.HttpContext.Response.ExtAddVersionInHeaderResponse(
                    this._Configuration.GetSection("AppSettings:Version").Value
                );

                await context.HttpContext.Response.WriteAsync(
                    new StatusCodeModel(context.HttpContext.Response.StatusCode).ToString()
                );
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            app.UseSwaggerUI(l =>
            {
                l.SwaggerEndpoint(
                    url: "../swagger/v1/swagger.json",
                    name: "WebApiRest.NetCore v1 " + env.EnvironmentName
                );
            });

            app.UseCors(l =>
            {
                l.AllowAnyOrigin();
                l.AllowAnyMethod();
                l.AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}