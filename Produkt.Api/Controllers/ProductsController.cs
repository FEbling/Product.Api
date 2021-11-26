using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Product.Api.IService;

namespace Product.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly ILogger<ProductsController> _logger;
    private IProductsService Service { get; }
    

    public ProductsController( ILogger<ProductsController> logger, IProductsService service ) {
      _logger = logger;
      Service = service;
    }


    /// <summary>
    /// Get one product identified by Id
    /// </summary>
    /// <param name="id">ProductId</param>
    /// <returns>productData</returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Model.Product>> GetAsync( [FromRoute] uint id ) {
      try {
        return await Service.ReadAsync(id);
      }
      catch(KeyNotFoundException) {
        return NotFound();
      }
      catch(Exception e) {
        _logger.LogError( e.Message );
        throw;
      }
    }


    /// <summary>
    /// Returns all available products
    /// </summary>
    /// <returns>List of productData</returns>
    [HttpGet]
    public async Task<List<Model.Product>> GetAllAsync() {
      try {
        return await Service.ReadAllAsync();
      }
      catch(Exception e) {
        _logger.LogError(e.Message);
        throw;
      }
    }


    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="product">ProductData</param>
    /// <returns>productData</returns>
    [HttpPut]
    public async Task<ActionResult<Model.Product>> PutAsync([FromBody] Model.Product product ) {
      if (!ModelState.IsValid)
        return BadRequest();
      try {
        return await Service.UpdateAsync(product);
      }
      catch(KeyNotFoundException) {
        return NotFound();
      }
      catch(Exception e) {
        _logger.LogError(e.Message);
        throw;
      }
    }


    /// <summary>
    /// Add a new product
    /// </summary>
    /// <param name="product">ProductData</param>
    /// <returns>productData</returns>
    [HttpPost]
    public async Task<ActionResult<Model.Product>> PostAsync([FromBody] Model.Product product)
    {
      if(!ModelState.IsValid)
        return BadRequest();
      try {
        return await Service.WriteAsync(product);
      }
      catch(DuplicateNameException) {
        return new StatusCodeResult((int)HttpStatusCode.Conflict);
      }
      catch(Exception e) {
        _logger.LogError(e.Message);
        return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
      }
    }


    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">ProductId</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] uint id)
    {
      try {
        await Service.DeleteAsync(id);
      }
      catch(KeyNotFoundException) {
        return NotFound();
      }
      catch(Exception e) {
        _logger.LogError(e.Message);
        return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
      }

      return Ok();
    }


  }
}
