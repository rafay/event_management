using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvenetManagement.API.Models;
using EvenetManagement.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;

namespace EvenetManagement.API
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
            services.Configure<DatabaseSettings>(
                Configuration.GetSection("DataBaseSettings"));

            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = AzureADDefaults.BearerAuthenticationScheme;
            })
                .AddAzureADBearer(options => Configuration.Bind("AzureActiveDirectory", options));

            //IdentityModelEventSource.ShowPII = true;

            string corsDomains = "http://localhost:4200";
            string[] domains = corsDomains.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            services.AddCors(o => o.AddPolicy("AppCORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(domains);
            }));

            services.AddSingleton<IDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<IEventsService, EventsService>();



            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AppCORSPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
