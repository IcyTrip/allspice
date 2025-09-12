[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecipesController : ControllerBase
{
    private readonly RecipeService _service;
    private readonly Auth0Provider _auth0Provider;
    public RecipesController(RecipeService service, Auth0Provider auth0Provider)
    {
        _service = service;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<Recipe>> GetRecipes()
    {
        try
        {
            var recipes = _service.GetRecipes();
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<Recipe> GetById(int id)
    {
        try
        {
            var recipe = _service.GetRecipeById(id);
            if (recipe == null)
            {
                return BadRequest("Recipe does not exist.");
            }
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipe)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipe.CreatorId = userInfo.Id;

            var newRecipe = _service.CreateRecipe(recipe);
            return Ok(newRecipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteRecipe(int id)
    {
        try
        {
            var result = _service.DeleteRecipe(id);
            if (!result)
            {
                return BadRequest("Error deleting recipe.");
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Recipe> UpdateRecipe(int id, [FromBody] Recipe recipe)
    {
        try
        {
            var updatedRecipe = _service.UpdateRecipe(id, recipe);
            return Ok(updatedRecipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}