using System.ComponentModel.DataAnnotations;
namespace CornerStore.Models.DTOs;

public class OrderProductDTO
{
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    [Required]
    public decimal Quantity { get; set; }
}