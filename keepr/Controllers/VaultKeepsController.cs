namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider _auth;
    private readonly VaultsService _VaultService;
    private readonly VaultKeepsService _VaultKeepsService;
    private readonly KeepsService _ks;


    public VaultKeepsController(Auth0Provider auth, VaultsService VaultService, VaultKeepsService vaultKeepsService, KeepsService ks)
    {
        _auth = auth;
        _VaultService = VaultService;
        _VaultKeepsService = vaultKeepsService;
        _ks = ks;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _VaultService.GetOne(vaultKeepData.VaultId, userInfo.Id);
            Keep keep = _ks.GetKeepById(vaultKeepData.KeepId);
            keep.Kept++;
            if (userInfo == null || vault.CreatorId != userInfo.Id) { throw new Exception("You do not have permission to add to this vault."); }
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep newVaultKeep = _VaultKeepsService.Create(vaultKeepData, userInfo?.Id);
            return Ok(newVaultKeep);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Delete(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _VaultKeepsService.Remove(id, userInfo.Id);
            return Ok("Vault Keep Removed ✨");
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }


}
