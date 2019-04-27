using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestAPI.Filters;
using TestAPI.Models;

namespace TestAPI
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
            services.AddMvc(options=> { options.Filters.Add<JsonExceptionFilter>(); options.Filters.Add<RequireHttpsOrCloseAttribute>(); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //to give the path to swagger document
            services.AddSwaggerDocument();

            //services.AddDbContext<pubsContext>(options => { options.UseInternalServiceProvider(_serviceprovider).});

            //For API versioning
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //This will select highest version
                //options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.ApiVersionSelector = new LowestImplementedApiVersionSelector(options);
            });
            //to make url lowercase
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUi3(options => {
                    options.DocumentPath = "/swagger/v1/swagger.json";
                });
                app.UseApiVersioning();
               
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }
}
