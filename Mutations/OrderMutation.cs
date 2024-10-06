using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SOPGraphQL.DTOs;

namespace SOPGraphQL.Mutations;

public class OrderMutation
{
    
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public OrderMutation(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Order?> CreateOrderAsync(OrderDTO newOrderDto)
    {
        Order order = _mapper.Map<Order>(newOrderDto);
        var check = await _context.Orders.FirstOrDefaultAsync(p => p.UserId == newOrderDto.UserId && p.OrderTime == newOrderDto.OrderTime);
        if (check == null)
        {
            try
            {
                var entry = await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while creating order: {ex.Message}");
                return null;
            }
        }
        return null;
    }

    public async Task<OrderDTO?> UpdateOrderAsync(Guid id, OrderDTO orderDto)
    {
        var affectedRows = await _context.Orders
            .Where(o => o.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(o => o.Status, orderDto.Status)
                .SetProperty(o => o.TotalCost, orderDto.TotalCost)
                .SetProperty(o => o.OrderTime, orderDto.OrderTime));

        if (affectedRows == 0)
        {
            throw new Exception("Order not found");
        }

        var entry = _mapper.Map<OrderDTO>(await _context.Orders.FindAsync(id));
        return entry;
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var affectedRows = await _context.Orders
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return affectedRows > 0;
    }
}