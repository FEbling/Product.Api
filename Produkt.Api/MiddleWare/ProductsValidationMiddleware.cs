using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Product.Api.MiddleWare
{
  public sealed class ProductsValidationMiddleware
  {
    private readonly RequestDelegate _next;

    public ProductsValidationMiddleware(RequestDelegate next) {
      _next = next;
    }


    public async Task Invoke(HttpContext context) {
      // is json type?
      if(context?.Request?.ContentType?.Contains("application/json") ?? false) {
        var body = context.Request.Body;
        using(var reader = new StreamReader(body, Encoding.UTF8)) {
          var jsonData = await reader.ReadToEndAsync();
          var product = JsonConvert.DeserializeObject<Model.Product>(jsonData);
          // check if body is a product
          if(product != null) {
            if(product.Category == null) {
              context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
              await context.Response.WriteAsync( "Category is empty" );
              return;
            }
          }
        }

        body.Seek(0, SeekOrigin.Begin);

        await _next(context);

      }
    }
  }
}
