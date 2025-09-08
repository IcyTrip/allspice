public class FavoriteService
{
    private readonly FavoritesRepository _repo;

    public FavoriteService(FavoritesRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Favorite> GetFavorites()
    {
        return _repo.GetAll().ToList();
    }

    public Favorite CreateFavorite(Favorite favorite)
    {
        return _repo.Create(favorite);
    }

    public Favorite GetFavoriteById(int id)
    {
        return _repo.GetById(id);
    }

    public bool DeleteFavorite(int id)
    {
        return _repo.Delete(id);
    }
}