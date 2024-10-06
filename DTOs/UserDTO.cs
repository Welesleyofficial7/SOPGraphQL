using System.ComponentModel.DataAnnotations;

namespace SOPGraphQL.DTOs;

public class UserDTO
{
    
    [Required]
    [MaxLength(36)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [MaxLength(256)]
    public string DestinationAddress { get; set; }

    public UserDTO(string name, string email, string phoneNumber, string destinationAddress)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        DestinationAddress = destinationAddress;
    }

}
