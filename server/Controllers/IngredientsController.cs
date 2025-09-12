[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IngredientsController : ControllerBase
{
    private readonly IngredientService _service;
    private readonly Auth0Provider _auth0Provider;
    public IngredientsController(IngredientService service, Auth0Provider auth0Provider)
    {
        _service = service;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<IEnumerable<Ingredient>> GetIngredients()
    {
        try
        {
            var ingredients = _service.GetIngredients();
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredient)
    {
        try
        {
            Ingredient newIngredient = _service.CreateIngredient(ingredient);
            return Ok(newIngredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<Ingredient> GetById(int id)
    {
        try
        {
            var ingredient = _service.GetIngredientById(id);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("recipe/{id}")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<Ingredient>> GetAllIngredientsById(int id)
    {
        try
        {
            var ingredients = _service.GetAllIngredientsById(id);
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteIngredient(int id)
    {
        try
        {
            var result = _service.DeleteIngredient(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}