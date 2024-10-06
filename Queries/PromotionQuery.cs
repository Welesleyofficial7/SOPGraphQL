namespace SOPGraphQL.Queries;

[ExtendObjectType("Query")]
public class PromotionQuery
{
    private readonly ApplicationContext _context;

    public PromotionQuery([Service] ApplicationContext context)
    {
        _context = context;
    }

    public IQueryable<Promotion> GetAllPromotions() => _context.Promotions;

    public Promotion GetPromotionById(Guid id) => _context.Promotions.Find(id);
}