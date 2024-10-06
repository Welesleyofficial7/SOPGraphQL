using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SOPGraphQL.DTOs;

namespace SOPGraphQL.Mutations;

public class PromotionMutation
{
    
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public PromotionMutation(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Promotion?> CreatePromotionAsync(PromotionDTO newPromotionDto)
    {
        Promotion promotion = _mapper.Map<Promotion>(newPromotionDto);
        var check = await _context.Promotions.FirstOrDefaultAsync(p => p.Code == newPromotionDto.Code);
        if (check == null)
        {
            try
            {
                var entry = await _context.Promotions.AddAsync(promotion);
                await _context.SaveChangesAsync();
                return entry.Entity;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while creating promotion: {ex.Message}");
                return null;
            }
        }
        return null;
    }

    public async Task<PromotionDTO?> UpdatePromotionAsync(Guid id, PromotionDTO promotion)
    {
        var affectedRows = await _context.Promotions
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(p => p.Code, promotion.Code)
                .SetProperty(p => p.Discount, promotion.Discount)
                .SetProperty(p => p.StartDate, promotion.StartDate)
                .SetProperty(p => p.EndDate, promotion.EndDate));

        if (affectedRows == 0)
        {
            throw new Exception("Promotion not found");
        }

        var entry = _mapper.Map<PromotionDTO>(await _context.Promotions.FindAsync(id));
        return entry;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var affectedRows = await _context.Promotions
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return affectedRows > 0;
    }
}