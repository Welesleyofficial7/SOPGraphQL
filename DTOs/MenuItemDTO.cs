using System.ComponentModel.DataAnnotations;
using SOPGraphQL.Models.Utils;

namespace SOPGraphQL.DTOs;

public class MenuItemDTO
{

    [Required]
    [MaxLength(36)]
    public string Name { get; set; }

    [MaxLength(512)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(50)]
    public Category Category { get; set; }

    public MenuItemDTO(string name, string description, decimal price, Category category)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.Category = category;
    }

}