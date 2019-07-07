using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using WebApiRest.NetCore.Models;
using WebApiRest.NetCore.Models.Dao;
using WebApiRest.NetCore.Models.DaoImpl.SQLServer;
using WebApiRest.NetCore.Models.Helpers;

namespace WebApiRest.NetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(l =>
                {
                    l.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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
                                            Configuration.GetSection("AppSettings:SecurityKey").Value
                                         )
                  };
              });

            // Set context
            services
              .AddDbContext<Models.Entity.SQLServer.DataBaseSQLServerContext>(options => {
                  options.UseSqlServer(
                        Configuration.GetConnectionString("DataBaseSQLServerContextConnectionString")
                    );
              })
              .AddDbContext<Models.Entity.MySQL.DataBaseMySQLContext>(options => {
                  options.UseMySQL(
                        Configuration.GetConnectionString("TestDBMySqlEntities")
                    );
              });

            // Add independency injection
            services
              .AddScoped<IAuthorizationDao, AuthorizationDaoImpl>()
              .AddScoped<IUserDao, UserDaoImpl>()
              .AddScoped<IMenuDao, MenuDaoImpl>()
              .AddScoped<IRoleDao, RoleDaoImpl>()
              .AddScoped<IGroupMenuDao, GroupMenuDaoImpl>();

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
            app.UseStatusCodePages(async context => {
                context.HttpContext.Response.ContentType = "application/json";

                await context.HttpContext.Response.WriteAsync(
                    new StatusCode(context.HttpContext.Response.StatusCode).ToString()
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