namespace SOPGraphQL.Queries;

[ExtendObjectType("Query")]
public class OrderItemQuery
{
    private readonly ApplicationContext _context;

    public OrderItemQuery([Service] ApplicationContext context)
    {
        _context = context;
    }

    public IQueryable<OrderItem> GetAllOrderItems() => _context.OrderItems;

    public OrderItem GetOrderItemById(Guid id) => _context.OrderItems.Find(id);
    
}