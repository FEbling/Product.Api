using System.Collections.Generic;
using System.Threading.Tasks;


namespace Product.Api.IDataAccess {
  public interface IProductsDa {
    Task<List<Model.Product>> ReadAllAsync();
    Task<Model.Product> ReadAsync(uint id);
    Task<Model.Product> WriteAsync(Model.Product product);
    Task<Model.Product> UpdateAsync(Model.Product product);
    Task DeleteAsync(uint id);
  }
}
