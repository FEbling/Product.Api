using System.ComponentModel.DataAnnotations;

namespace Product.Api.Model {
  public class ProductCategory {
    [Required]
    public uint Id { get; set; }

    [Required]
    [MaxLength(256, ErrorMessage = "Name is too long")]
    public string Name { get; set; }
  }
}
