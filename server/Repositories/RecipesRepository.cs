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
        string sql = @"SELECT Recipe.*, Account.*
        FROM Recipe
        JOIN Account ON Account.id = Recipe.creator_id
        ORDER BY Recipe.id ASC;";
        return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, splitOn: "id").ToList();
    }

    public Recipe Create(Recipe data)
    {
        string sql = @"
            INSERT INTO Recipe (title, instructions, img, category, creator_id)
            VALUES (@Title, @Instructions, @Img, @Category, @CreatorId);
            SELECT Recipe.*
            FROM Recipe
            WHERE Recipe.id = LAST_INSERT_ID();";

        int id = _db.ExecuteScalar<int>(sql, data);
        return GetById(id);
    }

    public bool Delete(int id)
    {
        int rows = _db.Execute("DELETE FROM Recipe WHERE Id = @id;", new { id });
        return rows > 0;
    }

    public Recipe GetById(int id)
    {
        string sql = @"
        SELECT Recipe.*, Account.*
        FROM Recipe
        JOIN Account ON Account.id = Recipe.creator_id
        WHERE Recipe.id = @id;";
        return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { id },splitOn: "id").FirstOrDefault();
    }

    public Recipe Update(int id, Recipe updateData)
    {
        updateData.Id = id;
        string sql = @"
        UPDATE Recipe
        SET
            title = @Title,
            instructions = @Instructions,
            img = @Img,
            category = @Category
        WHERE id = @id;
        SELECT * FROM Recipe WHERE id = @id;";

        var parameters = new
        {
            updateData.Id,
            updateData.Title,
            updateData.Instructions,
            updateData.Img,
            Category = updateData.Category.ToString()
        };

        return _db.QuerySingleOrDefault<Recipe>(sql, parameters);
    }
}