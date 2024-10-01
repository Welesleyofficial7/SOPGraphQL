using SOPBackend;

namespace SOPGraphQL.Queries;

public class OrderQuery
{
    private readonly ApplicationContext _context;

    public OrderQuery(ApplicationContext context)
    {
        _context = context;
    }

    public IQueryable<Order> GetAllOrders => _context.Orders;

    public Order GetOrderById(Guid id) => _context.Orders.Find(id);
}