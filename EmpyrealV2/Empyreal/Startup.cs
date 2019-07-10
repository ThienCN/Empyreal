using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Empyreal.Models;
using Microsoft.AspNetCore.Identity;
using Empyreal.Entities;
using Empyreal.Interfaces.Entities;
using Empyreal.Interfaces.Services;
using Empyreal.Services.Services;
using Empyreal.ServiceLocators;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Empyreal.Hubs;

namespace Empyreal
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
            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<EmpyrealContext>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EmpyrealContext>()
                .AddDefaultTokenProviders();

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
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
                options.ValidationInterval = TimeSpan.FromSeconds(0));

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

                options.LoginPath = "/Login/SignIn";
                options.AccessDeniedPath = "/Login/AccessDenied";
                options.SlidingExpiration = true;
            });


            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //
            // ConnectionString
            //services.AddDbContext<EmpyrealContext>(ServiceLifetime.Transient);
            services.AddDbContext<EmpyrealContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);


            /* Denpendency Injection*/

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            // Others Services
            services.AddSingleton<ICatalogService, CatalogService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductDetailService, ProductDetailService>();
            services.AddSingleton<IImageService, ImageService>();
            services.AddSingleton<IProductTypeService, ProductTypeService>();
            services.AddSingleton<IProductPriceService, ProductPriceService>();
            services.AddSingleton<IHistoryService, HistoryService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<ICartDetailService, CartDetailService>();
            services.AddSingleton<IRateService, RateService>();
            services.AddSingleton<ICommonService, CommonService>();
            services.AddSingleton<IProvinceService, ProvinceService>();
            services.AddSingleton<IDistrictService, DistrictService>();
            services.AddSingleton<IWardService, WardService>();
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderDetailService, OrderDetailService>();
            services.AddSingleton<IStatisticalService, StatisticalService>();

            services.AddSignalR();

            //// Login with Google
            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});

            //
            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
