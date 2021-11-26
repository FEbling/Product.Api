using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Product.Api.IDataAccess;

namespace Product.Api.DataAccess {
  public class ProductsDa : IProductsDa {

    private const string ProductsFile = "products.json";


    protected async Task WriteProductsToDisc( List<Model.Product> products ) {
      var json = JsonConvert.SerializeObject( products );
      await using var writer = new StreamWriter(ProductsFile);
      await writer.WriteAsync(json);
      await writer.FlushAsync();
    }


    protected async Task<List<Model.Product>> ReadProductsFromDisc() {
      using var reader = new StreamReader(ProductsFile);
      var json = await reader.ReadToEndAsync();
      var products = JsonConvert.DeserializeObject<List<Model.Product>>( json );
      return products ?? new List<Model.Product>();
    }


    public async Task<List<Model.Product>> ReadAllAsync() {
      var products = await 
        ReadProductsFromDisc();
      return products;
    }


    public async Task<Model.Product> ReadAsync(uint id) {
      var products = await ReadAllAsync();
      return products.Find(f => f.Id == id) 
             ?? throw new KeyNotFoundException();
    }


    public async Task<Model.Product> WriteAsync(Model.Product product) {
      var products = await ReadProductsFromDisc();
      if(product.Id == null) {
        var lastId = products.Max(f => f.Id);
        product.Id = lastId;
      }
      else {
        if(products.Any(f => f.Id == product.Id))
          throw new DuplicateNameException();
      }
      products.Add(product);
      await WriteProductsToDisc(products);

      return product;
    }


    public async Task<Model.Product> UpdateAsync(Model.Product product) {
      var products = await ReadAllAsync();
      var oldProductIndex = products.FindIndex(f => f.Id == product.Id);
      if(oldProductIndex == -1)
        throw new KeyNotFoundException();

      products[oldProductIndex] = product;
      await WriteProductsToDisc(products);

      return product;
    }


    public async Task DeleteAsync(uint id) {
      var products = await ReadAllAsync();
      var oldProductIndex = products.FindIndex(f => f.Id == id);
      if(oldProductIndex == -1)
        throw new KeyNotFoundException();

      products.RemoveAt(oldProductIndex);
      await WriteProductsToDisc(products);
    }
  }
}
