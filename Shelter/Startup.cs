using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Shelter.Models;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;



namespace Shelter
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

      services.AddDbContext<ShelterContext>(opt =>
          opt.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
      services.AddControllers();

      // registered the Swagger generator, defining one or more Swagger documents.
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Shelter",
          Version = "v1",
          Description = "Animal Shelter using ASP.NET Core Web API",

        });

      });


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {


        // exposed the generated Swagger as JSON endpoint(s) using middleware
        app.UseSwagger(c =>
        {
          c.SerializeAsV2 = true;
        });
        // inserted the swagger-ui middleware to expose interactive documentation, specifying the Swagger JSON endpoint(s) to power it from
        app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shelter API");
      //below lets home page localhost:5000 to swagger
      c.RoutePrefix = "";
    });
        //here you can put comments on swagger to assist others (between summary tags) with documentation below
        //        var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //      var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
        //     c.IncludeXmlComments(filePath);
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