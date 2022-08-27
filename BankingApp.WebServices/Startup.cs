using BankingApp.BusinessLayer.Contracts;
using BankingApp.BusinessLayer.Implementation;
using BankingApp.EFLayer.Contracts;
using BankingApp.EFLayer.Implementation;
using BankingApp.EFLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.WebServices
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

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience=true,
                    ValidAudience=Configuration["Jwt:Audience"],
                    ValidIssuer=Configuration["Jwt:Issuer"],
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            //Map ICustomerAsyncRepository
            services.AddTransient<ICustomerAsyncRepository, CustomerAsyncRepositoryImpl>();
            //Map ICustomerManager
            services.AddTransient<ICustomerAsyncManager, CustomerAsyncManager>();

            //Map IAccountRepository
            services.AddTransient<IAccountRepository, AccountRepositoryEFImpl>();
            //Map IAccountmanager
            services.AddTransient<IAccountManager, AccountManager>();

            //Map IUserAccountAsyncRepositary
            services.AddTransient<IUserAccountAsyncRepository, UserAccountAsyncRepositoryImpl>();

            //Map IUserAccountAsyncManager
            services.AddTransient<IUserAccountAsyncManager, UserAccountAsyncManager>();

            //Map ICustomerRepository
            services.AddTransient<ICustomerRepository, CustomerRepositoryEFImpl>();

            //Map ICustomerManager
            services.AddTransient<ICustomerManager, CustomerManager>();
            

            //Map Swagger
            services.AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name="Jwt Authentication",
                    Description="Enter Jwt Bearer token",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.Http,
                    Scheme="bearer",
                    BearerFormat="Jwt",
                    Reference=new OpenApiReference
                    {
                        Id=JwtBearerDefaults.AuthenticationScheme,
                        Type=ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme,new string[]{ } }
                });

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1.0",
                    Description = "API for banking service"
                });
                var xmlDoc = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlDoc);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking API V1");
                options.RoutePrefix = string.Empty;
            });

            //Configure Cors
            app.UseCors(options =>
            {
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
            );
        }
    }
}
