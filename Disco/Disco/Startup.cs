using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disco.Entities;
using Disco.Models;
using Disco.Repository.Contract;
using Disco.Repository.Implementation;
using Disco.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Disco
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc().AddFluentValidation();




            //overrid modelstate
            services.Configure<ApiBehaviorOptions>(options =>
           {
               options.InvalidModelStateResponseFactory = (DiscoContext) =>

               {
                   var errors = DiscoContext.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                   var result = new
                   {
                       Code = "666",
                       Message = "Validation errors",
                       Errors = errors
                   };
                   return new BadRequestObjectResult(result);
               };
            });
            
            services.AddDbContext<DiscoContext>(op => op.UseSqlServer(Configuration["ConnectionString:DiscoDB"]));
            services.AddScoped<IDiscoRepository<Member>, DiscoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
