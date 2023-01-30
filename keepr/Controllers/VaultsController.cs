namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly Auth0Provider _auth;
    private readonly VaultsService _VaultService;


    public VaultsController(Auth0Provider auth, VaultsService VaultService, VaultsService vaultVaultService)
    {
        _auth = auth;
        _VaultService = VaultService;

    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault VaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            VaultData.CreatorId = userInfo.Id;
            Vault Vault = _VaultService.Create(VaultData, userInfo.Id);
            Vault.Creator = userInfo;
            return Ok(Vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _VaultService.Get(id, userInfo?.Id);
            if (vault.IsPrivate == true)
            {
                throw new Exception("This vault is private.");
            }
            return Ok(vault);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.Id = id;
            Vault vault = _VaultService.Edit(vaultData, userInfo?.Id);
            return Ok(vault);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _VaultService.Delete(id, userInfo.Id);
            return Ok("Vault deleted");
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

//NOTE - look @ restaurant controller for example in helpReviews

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultKeep>>> GetVaultKeepsById(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            // if (accountId != null) { userInfo == accountId };
            List<VaultKeep> vaultKeeps = KeepsService.GetKeepsById(id, userInfo?.Id);
            return Ok(vaultKeeps);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
