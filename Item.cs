using System;

namespace Product.Api;

public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // Navigation property for related categories
    public ICollection<Category> Categories { get; set; } = new List<Category>();

}
