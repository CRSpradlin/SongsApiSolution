using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SongsAPI.Domain;
using SongsAPI.Profiles;
using SongsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI
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

            //Creating a mapper to help bridge the gap between services and code objects
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new SongsProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            //Configuring the mapper to be a singleton service so every http service doesn't create a new mapper each time
            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mapperConfig);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(pol =>
                {
                    pol.WithOrigins("http://localhost:4200"); //You can specify list of origins or all
                    pol.AllowAnyHeader();
                    pol.AllowAnyMethod(); //You can specify the methods allowed
                    pol.AllowCredentials();
                });
            });

            services.AddDbContext<SongsDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("songs")); //never ever hard-code connection string
            });

            // Abstract to concrete mapping, for production usage
            services.AddScoped<IProvideServerStatus, ChrisServerStatus>();
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SongsAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SongsAPI v1"));
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //Coming
            });
        }
    }
}
