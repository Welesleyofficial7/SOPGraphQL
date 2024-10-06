namespace SOPGraphQL.Queries;

[ExtendObjectType("Query")]
public class OrderQuery
{
    private readonly ApplicationContext _context;

    public OrderQuery([Service] ApplicationContext context)
    {
        _context = context;
    }

    public IQueryable<Order> GetAllOrders() => _context.Orders;

    public Order GetOrderById(Guid id) => _context.Orders.Find(id);
}