using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;



namespace Supermarket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        


        public void ConfigureServices(IServiceCollection services)
        {
            



            //services.AddDbContext<AppDbContext>(options =>
            //{


            //    string DataBase = $"Host={Environment.GetEnvironmentVariable("HOST")};" +
            //                       $"Port={Environment.GetEnvironmentVariable("PORT")};" +
            //                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
            //                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
            //                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};";



            //    options.UseNpgsql(DataBase);
            //});

            //services.AddDbContext<AppDbContext>(options =>
            //{


            //    string DataBase = $"Host={Environment.GetEnvironmentVariable("HOST")};" +
            //                       $"Port={Environment.GetEnvironmentVariable("PORT")};" +
            //                       $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
            //                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
            //                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};";



            //    options.UseNpgsql(DataBase);
            //});


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            

            string DataBase = $"Host={Environment.GetEnvironmentVariable("HOST")};" +
                                $"Port={Environment.GetEnvironmentVariable("PORT")};" +
                                $"Username={Environment.GetEnvironmentVariable("DB_USERNAME")};" +
                                $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                                $"Database={Environment.GetEnvironmentVariable("DB_NAME")}";

            services.AddSingleton<AppDbContext>(x => new AppDbContext(DataBase));

            



            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
