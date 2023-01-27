namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{

    private readonly Auth0Provider _auth;
    private readonly KeepsService _keepsService;
    private readonly VaultKeepsService _vaultKeepService;

    public KeepsController(Auth0Provider auth, KeepsService keepsService, VaultKeepsService vaultKeepsService)
    {
        _auth = auth;
        _keepsService = keepsService;
        _vaultKeepService = vaultKeepsService;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.Create(keepData);
            keep.Creator = userInfo;
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
