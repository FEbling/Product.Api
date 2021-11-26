using System.Collections.Generic;
using System.Threading.Tasks;
using Product.Api.IDataAccess;
using Product.Api.IService;

namespace Product.Api.Service {
  public class ProductsService : IProductsService {
    private IProductsDa DataAccess { get; }


    public ProductsService( IProductsDa dataAccess ) {
      DataAccess = dataAccess;
    }


    public async Task DeleteAsync(uint id) {
      await DataAccess.DeleteAsync( id );
    }


    public async Task<Model.Product> ReadAsync(uint id) {
      return await DataAccess.ReadAsync(id);
    }


    public async Task<List<Model.Product>> ReadAllAsync() {
      return await DataAccess.ReadAllAsync();
    }


    public async Task<Model.Product> WriteAsync(Model.Product product) {
      return await DataAccess.WriteAsync( product );
    }


    public async Task<Model.Product> UpdateAsync(Model.Product product) {
      return await DataAccess.UpdateAsync( product );
    }
  }
}
