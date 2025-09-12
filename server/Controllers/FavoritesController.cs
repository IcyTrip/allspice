[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly FavoriteService _service;
    private readonly Auth0Provider _auth0Provider;
    public FavoritesController(FavoriteService service, Auth0Provider auth0Provider)
    {
        _service = service;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Favorite>> GetFavorites()
    {
        try
        {
            var favorites = _service.GetFavorites();
            return Ok(favorites);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favorite)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            favorite.AccountId = userInfo.Id;

            var newFavorite = _service.CreateFavorite(favorite);
            return Ok(newFavorite);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Favorite> GetById(int id)
    {
        try
        {
            var favorite = _service.GetFavoriteById(id);
            return Ok(favorite);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteFavorite(int id)
    {
        try
        {
            var result = _service.DeleteFavorite(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}