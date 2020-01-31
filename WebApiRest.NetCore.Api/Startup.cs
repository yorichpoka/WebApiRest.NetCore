using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiRest.NetCore.Api.Filters;
using WebApiRest.NetCore.Bussiness.MySql;
using WebApiRest.NetCore.Bussiness.SqLite;
using WebApiRest.NetCore.Domain.Hubs;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.MongoDB;
using WebApiRest.NetCore.Domain.Interfaces.Bussiness.SqLite;
using WebApiRest.NetCore.Domain.Interfaces.Repositories;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.MongoDB;
using WebApiRest.NetCore.Domain.Interfaces.Repositories.SqLite;
using WebApiRest.NetCore.Domain.Models;
using WebApiRest.NetCore.Repositories.Contexts;
using WebApiRest.NetCore.Repositories.Repositories.MongoDB;
using WebApiRest.NetCore.Repositories.Repositories.SqLite;
using SqlServerBussinessPkg = WebApiRest.NetCore.Bussiness.SqlServer;
using SqlServerRepositoriesPkg = WebApiRest.NetCore.Repositories.Repositories.SqlServer;

namespace WebApiRest.NetCore.Api
{
    public class Startup
    {
        public IConfiguration _Configuration { get; }
        private readonly IHostingEnvironment _HostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this._Configuration = configuration;
            this._HostingEnvironment = hostingEnvironment;

            this.ConfigureSerilog(@"applog.txt");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<CookiePolicyOptions>(
                    options => {
                        options.CheckConsentNeeded = (context) => {
                                                        return true;
                                                     };
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    }
                );
            // Default configuration.
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(l =>
                {
                    l.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // Config cors
            services
                .AddCors(
                    options => {
                        options.AddPolicy("AllowAny", 
                            l => {
                                l.AllowAnyHeader();
                                l.AllowAnyMethod();
                                l.AllowAnyOrigin();
                                l.AllowCredentials();
                                l.WithOrigins("http://localhost:4200");
                            }
                        );
                    }
                );
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
                            Type = "apiKey"
                        }
                    );
                    // Add security requiement.
                    l.AddSecurityRequirement(
                        new Dictionary<string, IEnumerable<string>> {
                            { "Bearer", Enumerable.Empty<string>() },
                        }
                    );
                });

            // Set context
            services
              .AddDbContext<DataBaseSQLServerContext>(options =>
              {
                  options.UseSqlServer(
                        this._Configuration.GetConnectionString("TestDBSQLServer")
                    );
              })
              .AddDbContext<DataBaseMySQLContext>(options =>
              {
                  options.UseMySQL(
                        this._Configuration.GetConnectionString("TestDBMySql")
                    );
              })
              .AddDbContext<DataBaseSQLiteContext>(options =>
              {
                  options.UseSqlite(
                        this._Configuration.GetConnectionString("OpenDataWebSitesDBSQLite")
                    );
              });

            // Add AutoMapper.
            services
                .AddAutoMapper();

            // Add independency injection
            services
              // MySql || SqlServer
              .AddScoped<IAuthorizationRepository, SqlServerRepositoriesPkg.AuthorizationRepositoryImpl>()
              .AddScoped<IUserRepository, SqlServerRepositoriesPkg.UserRepositoryImpl>()
              .AddScoped<IMenuRepository, SqlServerRepositoriesPkg.MenuRepositoryImpl>()
              .AddScoped<IRoleRepository, SqlServerRepositoriesPkg.RoleRepositoryImpl>()
              .AddScoped<IGroupMenuRepository, SqlServerRepositoriesPkg.GroupMenuRepositoryImpl>()
              // MongoDb
              .AddScoped<IRestaurantRepository, RestaurantRepositoryImpl>()
              .AddScoped<IGradeRepository, GradeRepositoryImpl>()
              .AddScoped<ICoordinateRepository, CoordinateRepositoryImpl>()
              .AddScoped<IAddressRepository, AddressRepositoryImpl>()
              .AddSingleton<IMongoDatabase>(l =>
                    new MongoClient(
                        this._Configuration.GetSection("ConnectionStrings:RestaurantDBMongoDB:ConnectionString").Value
                    )
                    .GetDatabase(
                        this._Configuration.GetSection("ConnectionStrings:RestaurantDBMongoDB:Database").Value
                    )
                )
              // SqLite
              .AddScoped<IWebSiteRepository, WebSiteRepositoryImpl>();

            // Add independency injection
            services
              // MySql || SqlServer
              .AddScoped<IAuthorizationBussiness, SqlServerBussinessPkg.AuthorizationBussinessImpl>()
              .AddScoped<IUserBussiness, SqlServerBussinessPkg.UserBussinessImpl>()
              .AddScoped<IMenuBussiness, SqlServerBussinessPkg.MenuBussinessImpl>()
              .AddScoped<IRoleBussiness, SqlServerBussinessPkg.RoleBussinessImpl>()
              .AddScoped<IGroupMenuBussiness, SqlServerBussinessPkg.GroupMenuBussinessImpl>()
              // MongoDb
              .AddScoped<IRestaurantBussiness, RestaurantBussinessImpl>()
              .AddScoped<IGradeBussiness, GradeBussinessImpl>()
              .AddScoped<ICoordinateBussiness, CoordinateBussinessImpl>()
              .AddScoped<IAddressBussiness, AddressBussinessImpl>()
              // SqLite
              .AddScoped<IWebSiteBussiness, WebSiteBussinessImpl>();

            // Add filter in scope.
            services
                .AddScoped<CustomActionFilter>();

            // Config IIS
            // services
            //   .Configure<IISOptions>(opt => {
            //       opt.AutomaticAuthentication = false;
            //       opt.ForwardClientCertificate = false;
            //   });

            // Config SignalR
            services
                .AddSignalR();

            // Configure Jwt
            // this.ConfigureJwt(services);
            
            // Configure identity server 4
            this.ConfigureIdentityServer4(services);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureJwt(IServiceCollection services)
        {
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
                      IssuerSigningKey = this._Configuration.GetSection("AppSettings:SecurityKey").Value.ExtGetSymmetricSecurityKey()
                  };
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Custom error message.
            app.UseStatusCodePages(async context => {
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
            app.UseSwaggerUI(l => {
                l.SwaggerEndpoint(
                    url: "../swagger/v1/swagger.json",
                    name: "WebApiRest.NetCore v1 " + env.EnvironmentName
                );
            });

            app.UseCors("AllowAny");

            // Config endpoind SignalR
            app.UseSignalR(routes => {
                routes.MapHub<UserHub>("/userHub");
                routes.MapHub<RoleHub>("/roleHub");
                routes.MapHub<MenuHub>("/menuHub");
                routes.MapHub<GroupMenuHub>("/groupMenuHub");
                routes.MapHub<AuthorizationHub>("/authorizationHub");
            });

            app.UseAuthentication();

            app.UseMvc();
        }
        
        /// <summary>
        /// ...
        /// </summary>
        /// <param name="pathLogFile">...</param>
        private void ConfigureSerilog(string pathLogFile)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel
                            .Debug()
                                .MinimumLevel
                                .Override("Microsoft", LogEventLevel.Warning)
                                    .Enrich
                                    .FromLogContext()
                                        .WriteTo
                                        .File(pathLogFile)
                                        .CreateLogger(); 

            
            Log.Information("Starting up of application.");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="service"></param>
        private void ConfigureIdentityServer4(IServiceCollection services) 
        {
            var builder = 
                services
                    .AddAuthentication(
                        options => {
                            SetAuthenticationOptions(options);
                        }
                    )
                    .AddCookie()
                    .AddOpenIdConnect(
                        options => {
                            SetOpenIdConnectOptions(options);
                        }
                    );
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="options"></param>
        private void SetOpenIdConnectOptions(OpenIdConnectOptions options)
        {
            options.Authority = "http://localhost:5099";
            options.ClientId = "WebApiRest.NetCore.Api";
            options.RequireHttpsMetadata = false;
            options.Scope.Add("profile");
            options.Scope.Add("openid");
            options.ResponseType = "id_token";
            options.SaveTokens = true;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="options"></param>
        private void SetAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme;
        }
    }
}