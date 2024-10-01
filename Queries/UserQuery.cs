using SOPBackend;

namespace SOPGraphQL.Queries;

public class UserQuery
{
    private readonly ApplicationContext _context;

    public UserQuery(ApplicationContext context)
    {
        _context = context;
    }

    public IQueryable<User> GetAllUsers() => _context.Users;

    public User GetUserById(Guid id) => _context.Users.Find(id);
}