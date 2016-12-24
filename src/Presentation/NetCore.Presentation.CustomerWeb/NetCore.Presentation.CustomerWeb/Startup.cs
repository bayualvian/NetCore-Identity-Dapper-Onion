using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.Service.Customer.Identity;
using NetCore.Core.Entities.Identity.User;
using NetCore.Core.Entities.Identity.Role;
using Microsoft.AspNetCore.Identity;
using NetCore.Interface.Service.Identity;
using NetCore.Core.Entities;
using NetCore.Infrastructure.Data.Identity;
using NetCore.Core.Framework;
using System.IO;

namespace NetCore.Presentation.CustomerWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            string appsettingPath = Path.Combine(env.ContentRootPath, "appsettings.json");
            ConfigManager.GetInstance().Init(appsettingPath);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var userStore = new IdentityUserStore(
                new UserLoginRepository(),
                new UserRepository(),
                new UserClaimRepository(),
                new RoleRepository(),
                new UserRoleRepository());
            var roleStore = new IdentityRoleStore(new RoleRepository());
            var userPrincipalFactory = new IdentityUserPrincipalFactory();
            services.AddSingleton<IUserClaimsPrincipalFactory<User>>(userPrincipalFactory);
            services.AddSingleton<IUserStore<User>>(userStore);
            services.AddSingleton<IRoleStore<Role>>(roleStore);

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
