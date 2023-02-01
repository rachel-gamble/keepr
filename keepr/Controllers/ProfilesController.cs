namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly AccountService _aserv;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;
    private readonly Auth0Provider _au;

    public ProfilesController(AccountService aserv, KeepsService ks, VaultsService vs, Auth0Provider au)
    {
        _aserv = aserv;
        _ks = ks;
        _vs = vs;
        _au = au;
    }

    [HttpGet("{id}")]

    public ActionResult<Profile> GetProfileById(string id)
    {
        try
        {
            Profile profile = _aserv.GetProfileById(id);
            return Ok(profile);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
