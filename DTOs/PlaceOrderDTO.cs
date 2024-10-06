namespace SOPGraphQL.DTOs;

public class PlaceOrderDTO
{
    public Guid UserId { get; set; }
    
    public List<PlaceOrderItemDTO> Items { get; set; }
    
    public Guid PromotionId { get; set; }
}