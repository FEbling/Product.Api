using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Product.Api.IDataAccess;
using Product.Api.Service;

namespace Product.Api.Test.Service
{
  public class ProductServiceTest
  {
    [Test]
    public async Task ReadAsync_OK_Returns_Result() {
      var daMock = new Mock<IProductsDa>();
      daMock.Setup( f => f.ReadAsync( 1 ) ).ReturnsAsync( new Model.Product() );
      var testClass = new ProductsService( daMock.Object );

      var result = await testClass.ReadAsync(1);
      result.Should().NotBeNull();
    }


    [Test]
    public async Task ReadAsync_OK_Returns_Empty_Result() {
      var daMock = new Mock<IProductsDa>();
      var testClass = new ProductsService(daMock.Object);

      var result = await testClass.ReadAsync(1);
      result.Should().BeNull();
    }


    [Test]
    public async Task ReadAllAsync_OK_Returns_MultipleResults() {
      var daMock = new Mock<IProductsDa>();
      var testResult = new List<Model.Product> {
        new Model.Product(),
        new Model.Product(),
        new Model.Product()
      };
      daMock.Setup(f => f.ReadAllAsync()).ReturnsAsync(testResult);
      var testClass = new ProductsService(daMock.Object);

      var result = await testClass.ReadAllAsync();
      result.Should().NotBeNull();
      result.Count.Should().Be(3);
    }


    [Test]
    public async Task DeleteAsync_OK() {
      var daMock = new Mock<IProductsDa>();
      var testClass = new ProductsService(daMock.Object);

      await testClass.DeleteAsync(1);
      daMock.Verify( f => f.DeleteAsync( It.IsAny<uint>()));
    }


    [Test]
    public async Task WriteAsync_OK() {
      var daMock = new Mock<IProductsDa>();
      var testClass = new ProductsService(daMock.Object);
      var testData = new Model.Product();

      await testClass.WriteAsync(testData);
      daMock.Verify(f => f.WriteAsync(testData));
    }
  }
}
