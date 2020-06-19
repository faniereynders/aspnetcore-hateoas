using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicExample.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
#if NETCOREAPP3_0 || NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
namespace BasicExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

#if NETCOREAPP3_0 || NETCOREAPP3_1

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddControllers()
                .AddHateoas(options =>
                {
                    options
                       .AddLink<PersonDto>("get-person", p => new { id = p.Id })
                       .AddLink<List<PersonDto>>("create-person")
                       .AddLink<PersonDto>("update-person", p => new { id = p.Id })
                       .AddLink<PersonDto>("delete-person", p => new { id = p.Id });
                });
        }

#elif NETCOREAPP2_0

       public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddHateoas(options =>
                {
                    options
                       .AddLink<PersonDto>("get-person", p => new { id = p.Id })
                       .AddLink<List<PersonDto>>("create-person")
                       .AddLink<PersonDto>("update-person", p => new { id = p.Id })
                       .AddLink<PersonDto>("delete-person", p => new { id = p.Id });
                });
        }
#endif


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#if NETCOREAPP3_0 || NETCOREAPP3_1
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

#elif NETCOREAPP2_0
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
#endif
    }
}
