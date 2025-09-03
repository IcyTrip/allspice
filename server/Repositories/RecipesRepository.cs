using Microsoft.AspNetCore.DataProtection.Repositories;
using MySqlConnector;

public class RecipesRepository : IRepository<Recipe>
{
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    public IEnumerable<Recipe> GetAll()
    {
        string sql = "SELECT * FROM Recipe;";
        return _db.Query<Recipe>(sql).ToList();
    }

    public Recipe Create(Recipe data)
    {
        string sql = @"
            INSERT INTO Recipe (title, instructions, img, category, creator_id)
            VALUES (@Title, @Instructions, @Img, @Category, @CreatorId);
            SELECT Recipe.*, Account.*
            FROM Recipe
            JOIN Account ON Account.id = Recipe.creator_id
            WHERE Recipe.id = LAST_INSERT_ID();";

        Recipe recipe = _db.Query(sql, (Recipe recipe, Account account) =>
        {
            recipe.CreatorId = account.Id;
            return recipe;
        }, data).SingleOrDefault();

        return recipe;
    }

    public bool Delete(int id)
    {
        int rows = _db.Execute("DELETE FROM Recipe WHERE Id = @id;", new { id });
        return rows > 0;
    }

    public Recipe GetById(int id)
    {
        string sql = "SELECT * FROM Recipe WHERE Id = @id;";
        return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }

    public Recipe Update(int id, Recipe updateData)
    {
        updateData.Id = id;
        string sql = @"
        UPDATE Recipe
        SET title = @Title, instructions = @Instructions, img = @Img, category = @Category, creator_id = @CreatorId
        WHERE id = @id;";

        return _db.QuerySingle<Recipe>(sql, updateData);
    }
}