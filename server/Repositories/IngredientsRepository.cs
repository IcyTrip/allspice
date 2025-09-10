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
        string sql = "SELECT * From Ingredient;";
        return _db.Query<Ingredient>(sql).ToList();
    }

    public Ingredient Create(Ingredient data)
    {
        string sql = @"
        INSERT INTO Ingredient (id, created_at, updated_at, name, quantity, recipe_id)
        VALUES (@Id, @CreatedAt, @UpdatedAt, @Name, @Quantity, @RecipeId);
        SELECT Recipe.*, Ingredient.*
        FROM Ingredient
        JOIN Recipe ON Recipe.id = Ingredient.recipe_id
        WHERE Ingredient.id = LAST_INSERT_ID();";

        Ingredient ingredient = _db.Query(sql, (Recipe recipe, Ingredient ingredient) =>
        {
            ingredient.RecipeId = recipe.Id;
            return ingredient;
        }, data).SingleOrDefault();

        return ingredient;
    }

    public Ingredient GetById(int id)
    {
        string sql = "SELECT * FROM Ingredient WHERE Id = @id;";
        return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }

    public IEnumerable<Ingredient> GetAllById(int id)
    {
        string sql = "SELECT * FROM Ingredient WHERE recipe_id = @RecipeId;";
        return _db.Query<Ingredient>(sql, new { id }).ToList();
    }

    public bool Delete(int id)
    {
        int rows = _db.Execute("DELETE FROM Ingredient WHERE Id = @id;");
        return rows > 0;
    }
}