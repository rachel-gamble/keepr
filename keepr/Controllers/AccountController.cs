namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetMyVaults()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string id = userInfo.Id;
            List<Vault> vaults = _accountService.GetMyVaults(id);
            return Ok(vaults);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }
}