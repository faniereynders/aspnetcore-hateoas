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

namespace BasicExample
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
            services
                .AddMvc()
                .AddHateoas(options =>
                {
                    options
                       .AddLink<PersonDto>("get-person", p => new { id = p.Id })
                       .AddLink<List<PersonDto>>("create-person")
                       .AddLink<PersonDto>("update-person", p => new { id = p.Id }, p => !p.IsReadOnly)
                       .AddLink<PersonDto>("delete-person", p => new { id = p.Id }, p => !p.IsReadOnly);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
