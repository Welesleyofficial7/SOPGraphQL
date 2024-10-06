using Microsoft.EntityFrameworkCore;
using SOPGraphQL;
using SOPGraphQL.DTOs;
using AutoMapper;

namespace SOPGraphQL.Mutations;

public class UserMutation
{

    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public UserMutation(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<User?> CreateUser(UserDTO newUser)
    {
        User user = _mapper.Map<User>(newUser);
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == newUser.Email);
        if (existingUser != null)
        {
            Console.WriteLine("User already exists!");
            return null;
        }


        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while creating user: {ex.Message}");
            return null;
        }

        return user;
    }

    public async Task<UserDTO?> UpdateUserAsync(Guid id, UserDTO updatedUser)
    {
        var affectedRows = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(update => update
                .SetProperty(u => u.Name, updatedUser.Name)
                .SetProperty(u => u.Email, updatedUser.Email)
                .SetProperty(u => u.PhoneNumber, updatedUser.PhoneNumber)
                .SetProperty(u => u.DestinationAddress, updatedUser.DestinationAddress));

        if (affectedRows == 0)
        {
            throw new Exception("User not found!");
        }

        var entry = _mapper.Map<UserDTO>(await _context.Users.FindAsync(id));
        return entry;
    }


    public async Task<bool> DeleteUser(Guid id)
    {
        // var user = _context.Users.Find(id);
        // if (user == null)
        // {
        //     return false;
        // }
        //
        // _context.Users.Remove(user);
        // _context.SaveChanges();
        // return true;

        var affectedRows = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        return affectedRows > 0;
    }
}