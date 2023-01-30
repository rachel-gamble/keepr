namespace keepr.Services;

public class VaultKeepsService
{
    public readonly VaultKeepsRepository _repo;
    public readonly VaultsService _vaultsService;
    public readonly AccountService _accountService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, AccountService accountService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
        _accountService = accountService;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
    {
        if (userId == null)
        {
            throw new System.Exception("You must be logged in to post.");
        }
        Vault vault = AccountService.GetMyVaults(vaultKeepData.VaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new System.Exception("You do not have access.");
        }
        VaultKeep newVaultKeep = _repo.Create(vaultKeepData);

        return newVaultKeep;
    }

    internal VaultKeep Get(int id)
    {
        return _repo.Get(id);
    }

}
