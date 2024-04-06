using System.Reflection;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using Rainfall_API.Controllers;
using Rainfall_API.Services;

namespace Rainfall_API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services
            services.AddScoped<IRainfallSvc, RainfallSvc>();

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.EnableAnnotations();

                // hardcode for now, update later once everything is working.
                opt.SwaggerDoc("1.0", new OpenApiInfo 
                { 
                    Title = "Rainfall API", 
                    Version = "1.0",
                    Description = "An API which provides rainfall reading data",
                    Contact = new OpenApiContact
                    {
                        Name = "Sorted",
                        Url = new Uri("https://www.sorted.com")
                    }
                });

                opt.AddServer(new OpenApiServer
                {
                    Url = "https://localhost:7124",
                    Description = "Rainfall API HTTPS"
                });

                opt.AddServer(new OpenApiServer
                {
                    Url = "http://localhost:5188",
                    Description = "Rainfall API HTTP"
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/1.0/swagger.json", "Rainfall API v1.0");
                });
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
