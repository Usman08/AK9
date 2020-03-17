using AK9.Admin.Middlewares;
using AK9.AppHelper.Utils;
using AK9.BLL.Services;
using AK9.DAL.EntityModel;
using AK9.DAL.EntityModel.Entities;
using AK9.DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AK9.Admin
{
    public class Startup
    {
        private IHostingEnvironment _env { get; set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // ===== Add our DbContext ========
            services.AddDbContext<AK9Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // ===== Add Identity ========
            services.AddIdentity<User, Role>()
                .AddUserManager<AspNetUserManager<User>>()
                .AddEntityFrameworkStores<AK9Context>()
                .AddDefaultTokenProviders();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<ICertificationBLL, CertificationBLL>()
                .AddTransient<IPolicyBLL, PolicyBLL>()
                .AddTransient<IServiceBLL, ServiceBLL>()
                .AddTransient<IDropdownBLL, DropdownBLL>()
                .AddTransient<IViewRenderService, ViewRenderService>()
                .AddLogging();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                //// Lockout settings
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 10;
                //options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = false;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.LoginPath = "/Account/SignIn";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, AK9Context dbContext)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHttpsRedirection();
                app.ConfigureExceptionLoggingMiddleware();
                app.UseExceptionHandler("/Error/Index");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            AppHttpContext.Services = app.ApplicationServices;

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
            app.UseCookiePolicy();

            // ===== Create tables ======
            dbContext.Database.EnsureCreated();
        }
    }
}
