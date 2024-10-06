using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SOPGraphQL.DTOs;

namespace SOPGraphQL.Mutations;

public class MenuItemMutation
{
    
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public MenuItemMutation(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<MenuItem?> CreateMenuItemAsync(MenuItemDTO newMenuItemDto)
    {
        MenuItem menuItem = _mapper.Map<MenuItem>(newMenuItemDto);
        var check = await _context.MenuItems.FirstOrDefaultAsync(p => p.Name == newMenuItemDto.Name);
        if (check == null)
        {
            try
            {
                var entry = await _context.MenuItems.AddAsync(menuItem);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while creating menu item: {ex.Message}");
                return null;
            }
        }
        return null;
    }

    public async Task<MenuItemDTO?> UpdateMenuItemAsync(Guid id, MenuItemDTO menuItemDto)
    {
        var affectedRows = await _context.MenuItems
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(m => m.Name, menuItemDto.Name)
                .SetProperty(m => m.Description, menuItemDto.Description)
                .SetProperty(m => m.Category, menuItemDto.Category)
                .SetProperty(m => m.Price, menuItemDto.Price));

        if (affectedRows == 0)
        {
            throw new Exception("Menu item not found");
        }

        var entry = _mapper.Map<MenuItemDTO>(await _context.MenuItems.FindAsync(id));
        return entry;
    }

    public async Task<bool> DeleteMenuItemAsync(Guid id)
    {
        var affectedRows = await _context.MenuItems
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return affectedRows > 0;
    }
}