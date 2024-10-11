

using System.Text.Json.Serialization;

namespace Product.Api;

public class Category
{
    public int Id { get; set; }
    public int ItemId { get; set; }  // Foreign key for Item
    public string? SubCategory { get; set; }

    // Navigation property to Item
    [JsonIgnore]
    public Item? Item { get; set; }

}
