using System.ComponentModel.DataAnnotations;

public class Favorite
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int RecipeId { get; set; }
    [MaxLength(255)] public string AccountId { get; set; }
    
}