using System.ComponentModel.DataAnnotations;
namespace CornerStore.Models.DTOs;

public class OrderProductDTO
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public decimal Quantity { get; set; }
    public ProductDTO Product { get; set; }
}