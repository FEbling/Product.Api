using System.ComponentModel.DataAnnotations;

namespace Product.Api.Model {
  public class Product {
    public uint? Id { get; set; }

    [Required]
    [MaxLength(256, ErrorMessage = "Name is too long")]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string Type { get; set; }

    [Required]
    public ProductCategory Category { get; set; }
  }
}
