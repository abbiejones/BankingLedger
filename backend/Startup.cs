//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Cors;
using BankingLedger.DataAccess;
using BankingLedger.Biz;
using BankingLedger.Log;
using Microsoft.EntityFrameworkCore;


namespace BankingLedger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            
            services.AddEntityFrameworkSqlite();
            services.AddDbContext<BankingContext>(options => 
                    options.UseSqlite("DataSource=bank.db"));
            
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserBiz, UserBiz>();
            services.AddScoped<IBankAccountBiz, BankAccountBiz>();
            services.AddScoped<ILogger,Logger>();
            services.AddOptions();
            //services.BuildServiceProvider();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }

            // app.UseCors(builder =>
            //     builder.WithOrigins("http://localhost")
            //     .AllowAnyHeader());

            //app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyCredentials());
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());           
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}