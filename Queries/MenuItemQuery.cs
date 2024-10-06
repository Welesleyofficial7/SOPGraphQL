namespace SOPGraphQL.Queries;

[ExtendObjectType("Query")]
public class MenuItemQuery
{
    private readonly ApplicationContext _context;

    public MenuItemQuery([Service] ApplicationContext context)
    {
        _context = context;
    }

    public IQueryable<MenuItem> GetAllMenuItems() => _context.MenuItems;

    public MenuItem GetMenuItemById(Guid id) => _context.MenuItems.Find(id);
}