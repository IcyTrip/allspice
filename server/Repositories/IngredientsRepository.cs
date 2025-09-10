using MySqlConnector;

public class IngredientsRepository
{
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    public IEnumerable<Ingredient> GetAll()
    {
        string sql = @"SELECT Ingredient.*, Recipe.*
        From Ingredient
        JOIN Recipe ON Recipe.id = Ingredient.recipe_id;";
        return _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
        {
            ingredient.Recipe = recipe;
            return ingredient;
        }, splitOn: "id").ToList();
    }

    public Ingredient Create(Ingredient data)
    {
        string sql = @"
        INSERT INTO Ingredient (name, quantity, recipe_id)
        VALUES (@Name, @Quantity, @RecipeId);
        SELECT Ingredient.*
        FROM Ingredient
        WHERE Ingredient.id = LAST_INSERT_ID();";

        return _db.Query<Ingredient>(sql, data).SingleOrDefault();
    }

    public Ingredient GetById(int recipeId)
    {
        string sql = "SELECT * FROM Ingredient WHERE recipe_id = @RecipeId;";
        return _db.QueryFirstOrDefault<Ingredient>(sql, new { recipeId });
    }

    public IEnumerable<Ingredient> GetAllById(int id)
    {
        string sql = "SELECT * FROM Ingredient WHERE recipe_id = @RecipeId;";
        return _db.Query<Ingredient>(sql, new { id }).ToList();
    }

    public bool Delete(int id)
    {
        int rows = _db.Execute("DELETE FROM Ingredient WHERE Id = @id;", new { id });
        return rows > 0;
    }
}