using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SOPGraphQL.DTOs;

namespace SOPGraphQL.Mutations;

public class OrderItemMutation
{
    
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public OrderItemMutation(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<OrderItem?> CreateOrderItemAsync(OrderItemDTO newOrderItemDto)
    {
        OrderItem orderItem = _mapper.Map<OrderItem>(newOrderItemDto);
        var check = await _context.OrderItems.FirstOrDefaultAsync(p => p.OrderId == newOrderItemDto.OrderId && p.MenuItemId == newOrderItemDto.MenuItemId);
        if (check == null)
        {
            try
            {
                var entry = await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while creating order item: {ex.Message}");
                return null;
            }
        }
        return null;
    }

    public async Task<OrderItemDTO?> UpdateOrderItemAsync(Guid id, OrderItemDTO orderItemDto)
    {
        var affectedRows = await _context.OrderItems
            .Where(o => o.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(o => o.Quantity, orderItemDto.Quantity)
                .SetProperty(o => o.Subtotal, orderItemDto.Subtotal));

        if (affectedRows == 0)
        {
            throw new Exception("Order item not found");
        }

        var entry = _mapper.Map<OrderItemDTO>(await _context.OrderItems.FindAsync(id));
        return entry;
    }

    public async Task<bool> DeleteOrderItemAsync(Guid id)
    {
        var affectedRows = await _context.OrderItems
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return affectedRows > 0;
    }
}