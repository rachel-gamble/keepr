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

    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
        try
        {
            List<Keep> keep = _keepsService.Get();
            return Ok(keep);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
        try
        {
            Keep keep = _keepsService.Get(id);
            return Ok(keep);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.Id = id;
            Keep keep = _keepsService.Edit(keepData, userInfo?.Id);
            return Ok(keep);
        }
        catch (System.Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Delete(int id)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            _keepsService.Delete(id, userInfo.Id);
            return Ok("Keep deleted");
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
