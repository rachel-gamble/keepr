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
        // Vault vault = _accountService.GetMyVaults(userId);
        // if (vault.CreatorId != userId)
        // {
        //     throw new System.Exception("You do not have access.");
        // }
        VaultKeep newVaultKeep = _repo.Create(vaultKeepData);

        return newVaultKeep;
    }

    internal VaultKeep Get(int id)
    {
        return _repo.Get(id);
    }

    internal void Remove(int id, string userId)
    {
        VaultKeep found = Get(id);
        if (found.CreatorId != userId)
        {
            throw new Exception("You do not have permission to remove this Vault Keep.");
        }
        _repo.Remove(id);
    }

}
