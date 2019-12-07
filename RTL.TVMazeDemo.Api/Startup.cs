using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTL.TVMaze.BLL.Repositories;
using RTL.TVMaze.BLL.Services;
using RTL.TVMaze.Dal.EFCore;
using RTL.TVMaze.Dal.EFCore.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace RTL.TVMazeDemo.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("localDb")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("RTL.TVMaze.Api", new Info() { Title = "RTL TVMaze Api", Description = "Api for RTL TVMaze " });
                // XML Documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddHttpClient();
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<ICastCreditRepository, CastCreditRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();
            services.AddScoped<ShowService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/RTL.TVMaze.Api" + "/swagger.json", "RTL.TVMaze.Api");
            });
        }
    }
}
