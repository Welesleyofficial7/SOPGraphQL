using System.ComponentModel.DataAnnotations;

namespace SOPGraphQL.DTOs;

public class OrderItemDTO
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public Guid MenuItemId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Subtotal { get; set; }

    public OrderItemDTO(Guid orderId, Guid menuItemId, int quantity, decimal subtotal)
    {
        OrderId = orderId;
        MenuItemId = menuItemId;
        Quantity = quantity;
        Subtotal = subtotal;
    }

}