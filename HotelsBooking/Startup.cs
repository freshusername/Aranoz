using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using HotelsBooking.Mapping;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCore.Managers;

namespace HotelsBooking
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
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Infrastructure")));

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1440);

                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //var config = new AutoMapper.MapperConfiguration(c =>
            //{
            //    c.AddProfile(new ApplicationMappingProfile());
            //});

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IAdminManager, AdminManager>();
            services.AddTransient<IHotelManager, HotelManager>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IAdditionalConvManager, AdditionalConvManager>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IHotelManager, HotelManager>();
            services.AddTransient<IProfileManager, ProfileManager>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IAdminRoomManager, AdminRoomManager>();
            services.AddTransient<IConvsManager, ConvsManager>();

      //var mapper = config.CreateMapper();


      services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                Configuration.GetConnectionString("DefaultConnection")));

    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
