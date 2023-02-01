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

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
        try
        {
            _aserv.GetProfileById(profileId);
            List<Keep> keeps = _ks.GetByCreatorId(profileId);
            return Ok(keeps);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileIdAsync(string profileId)
    {
        try
        {
            Account userInfo = await _au.GetUserInfoAsync<Account>(HttpContext);
            Profile profile = _aserv.GetProfileById(profileId);
            List<Vault> vaults = _vs.GetVaultsByCreatorId(profileId, userInfo?.Id);
            return Ok(vaults);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
