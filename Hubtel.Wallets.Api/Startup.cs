using Hubtel.Wallets.Api.Models.BusinessRule;
using Hubtel.Wallets.Api.Models.Data.Service;
using Hubtel.Wallets.Api.Models.Data.WalletDBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Hubtel.Wallets.Api
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
            services.AddControllers();
           
            //Connectionstring 
            services.AddDbContext<WalletDBContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Conn"));
            });

            //life time of the dependency injection
            services.AddScoped<WalletService>();
            services.AddScoped<WalletRules>();

            //Swagger
            services.AddSwaggerGen(options => {

                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Hubtel API",
                        Description = "This API service is used to manage a user's wallet on the Hubtel app.",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Ishmael Kwaw Obeng",
                            Email = "electrons500@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/ishmael-obeng") 
                        }
                    });

                //Generate xml document
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hubtel API");

            });


        }
    }
}
