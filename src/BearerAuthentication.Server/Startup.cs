using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BearerAuthentication.Server.Authentication;
using BearerAuthentication.Server.Models;
using BearerAuthentication.Server.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BearerAuthentication.Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicy(
                    new List<IAuthorizationRequirement>
                    {
                        new UserRequirement()
                    },
                    new[] { BearerAuthenticationDefaults.AuthenticationScheme });
            });

            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IUserService userService)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });

            app.UseBearerAuthentication(options =>
            {
                options.Events = new BearerAuthenticationEvents
                {
                    OnSigningIn = async context =>
                    {
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Signing in...");
                        });
                    },
                    OnSignedIn = async context =>
                    {
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Signed in...");
                        });
                    },
                    OnValidatePrincipal = async context =>
                    {
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Validate principal...");
                            var userIdClaim = context.Principal.FindFirst(x => x.Type == "UserId");
                            int userId;
                            User user;
                            if (userIdClaim == null ||
                                !int.TryParse(userIdClaim.Value, out userId) ||
                                (user = userService.GetUser(userId)) == null ||
                                !user.IsActive)
                                context.RejectPrincipal();
                        });
                    },
                    OnSigningOut = async context =>
                    {
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Signing out...");
                        });
                    },
                    OnUnauthorized = async context =>
                    {
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Unauthorized...");
                        });
                    },
                    OnForbidden = async context =>
                    {
                        await Task.Run(() =>
                        {
                            Debug.WriteLine("Forbidden...");
                        });
                    }
                };
            });

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
