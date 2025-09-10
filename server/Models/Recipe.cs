using System.ComponentModel.DataAnnotations;

public class Recipe
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [MaxLength(255)] public string Title { get; set; }
    [MaxLength(5000)] public string Instructions { get; set; }
    [Url, MaxLength(1000)] public string Img { get; set; }
    public RecipeCategory Category { get; set; }
    public string CreatorId { get; set; }
    public Account Creator { get; set; }
}

public enum RecipeCategory
{
    Breakfast,
    Lunch,
    Dinner,
    Snack,
    Dessert
}