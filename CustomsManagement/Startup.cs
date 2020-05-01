using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomersManagement.Data;
using CustomersManagement.Infrastructure;
using CustomersManagement.IRepository;
using CustomersManagement.Repository;
using CustomersManagement.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CustomsManagement
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
            //buraya bağımlılıklar ve token eklenebilir

            var appSettingsSection = Configuration.GetSection("Tokens");
            services.Configure<Tokens>(appSettingsSection);

            // JWT authentication Aayarlaması
            var token = appSettingsSection.Get<Tokens>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Key);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                  JwtBearerDefaults.AuthenticationScheme;
            }
           )
           .AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateAudience = true,
                   //ValidAudience = "heimdall.fabrikam.com",
                   ValidAudience = token.Audience,
                   ValidateIssuer = true,
                   //ValidIssuer = "west-world.fabrikam.com",
                   ValidIssuer = token.Issuer,
                   ValidateLifetime = true,
                   //ClockSkew=TimeSpan.Zero,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes("uzun ince bir yoldayım şarkısını buradan tüm sevdiklerime hediye etmek istiyorum mümkün müdür acaba?")),
                   ClockSkew = TimeSpan.Zero
               };

               options.Events = new JwtBearerEvents
               {
                   OnTokenValidated = ctx => {
                       //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulama yapılabilir.
                       return Task.CompletedTask;
                   },
                   OnAuthenticationFailed = ctx => {
                       Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                       return Task.CompletedTask;
                   }
               };
           });

            services.AddMvc()
           .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            string sqlConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<CustomerContext>(options => {
                options.UseSqlServer(sqlConnectionString);
            });

            services.AddCors(configs => {
                configs.AddPolicy("Cemcir", bldr => {
                    bldr.AllowAnyHeader().
                    AllowAnyMethod().
                    AllowAnyOrigin();
                });
            });

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerDocumentation();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("Cemcir");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

    }
}
