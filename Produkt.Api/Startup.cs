using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.Api.DataAccess;
using Product.Api.IDataAccess;
using Product.Api.IService;
using Product.Api.MiddleWare;
using Product.Api.Service;

namespace Product.Api
{
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers();
      services.AddScoped<IProductsService, ProductsService>();
      services.AddScoped<IProductsDa, ProductsDa>();
      services.AddLogging();
      services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseMiddleware<ProductsValidationMiddleware>();
      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });

    }
  }
}
