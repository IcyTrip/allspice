public class RecipeService
{
    private readonly RecipesRepository _repo;
    public RecipeService(RecipesRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Recipe> GetRecipes()
    {
        return _repo.GetAll().ToList();
    }

    public Recipe GetRecipeById(int id)
    {
        return _repo.GetById(id);
    }

    public Recipe CreateRecipe(Recipe recipe)
    {
        return _repo.Create(recipe);
    }

    public bool DeleteRecipe(int id)
    {
        return _repo.Delete(id);
    }

    public Recipe UpdateRecipe(int id, Recipe recipe)
    {
        return _repo.Update(id, recipe);
    }
}