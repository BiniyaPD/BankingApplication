using BankingApp.BusinessLayer.Contracts;
using BankingApp.BusinessLayer.Implementation;
using BankingApp.EFLayer.Contracts;
using BankingApp.EFLayer.Implementation;
using BankingApp.EFLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp
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
            //Configure Entity Framework
            services.AddDbContext<db_bankContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            //config sessions
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(30);  //cookie expiration time
            });

            //Map ICustomerRepository
            services.AddTransient<ICustomerRepository, CustomerRepositoryEFImpl>();
            //Map ICustomerManager
            services.AddTransient<ICustomerManager, CustomerManager>();
            //Map ImanagerRepository
            services.AddTransient<IManagerRepository, ManagerRepositoryEFImpl>();
            //Map IBankmanager
            services.AddTransient<IBankManager, BankManager>();

            //Map IAccountRepository
            services.AddTransient<IAccountRepository, AccountRepositoryEFImpl>();
            //Map IAccountmanager
            services.AddTransient<IAccountManager, AccountManager>();
            //Map ITransactionRepository
            services.AddTransient<ITransactionRepository, TransactionRepositoryEFImpl>();
            services.AddTransient<ITransactionManager, TransactionManager>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
