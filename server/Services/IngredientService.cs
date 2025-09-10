public class IngredientService
{
    private readonly IngredientsRepository _repo;

    public IngredientService(IngredientsRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Ingredient> GetIngredients()
    {
        return _repo.GetAll().ToList();
    }

    public Ingredient CreateIngredient(Ingredient ingredient)
    {
        return _repo.Create(ingredient);
    }

    public Ingredient GetIngredientById(int id)
    {
        return _repo.GetById(id);
    }

    public IEnumerable<Ingredient> GetAllIngredientsById(int id)
    {
        return _repo.GetAllById(id);
    }

    public bool DeleteIngredient(int id)
    {
        return _repo.Delete(id);
    }
}