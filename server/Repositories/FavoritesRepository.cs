using Microsoft.AspNetCore.DataProtection.Repositories;
using MySqlConnector;

public class FavoritesRepository
{
    private readonly IDbConnection _db;
    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    public IEnumerable<Favorite> GetAll()
    {
        string sql = "SELECT * FROM Favorite;";
        return _db.Query<Favorite>(sql).ToList();
    }

    public Favorite Create(Favorite data)
    {
        string sql = @"
            INSERT INTO Favorite (account_id, recipe_id)
            VALUES (@AccountId, @RecipeId);
            SELECT Account.*, Recipe.*, Favorite.*
            FROM Favorite
            JOIN Account ON Account.id = Favorite.account_id
            JOIN Recipe ON Recipe.id = Favorite.recipe_id
            WHERE Favorite.id = LAST_INSERT_ID();";

        Favorite favorite = _db.Query(sql, (Account account, Recipe recipe, Favorite favorite) =>
        {
            favorite.AccountId = account.Id;
            favorite.RecipeId = recipe.Id;
            return favorite;
        }, data).SingleOrDefault();

        return favorite;
    }

    public Favorite GetById(int id)
    {
        string sql = "SELECT * FROM Favorite Where Id = @id;";
        return _db.QueryFirstOrDefault<Favorite>(sql, new { id });
    }

    public bool Delete(int id)
    {
        int rows = _db.Execute("DELETE FROM Favorite WHERE Id = @id;", new { id });
        return rows > 0;
    }
}