using System.ComponentModel.DataAnnotations;

public class Ingredient
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [MaxLength(255)] public string Name { get; set; }
    [MaxLength(255)] public string Quantity { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    
}