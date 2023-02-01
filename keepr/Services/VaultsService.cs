namespace keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;
    private readonly AccountService _aserv;

    public VaultsService(VaultsRepository repo, AccountService aserv)
    {
        _repo = repo;
        _aserv = aserv;
    }

    public Vault Create(Vault vaultData, string userId)
    {
        return _repo.Create(vaultData, userId);
    }

    internal List<Vault> GetByCreatorId(string profileId)
    {
        return _repo.GetVaultsByCreatorId(profileId);
    }


    // internal Vault Get(int id, string userId)
    // {
    //     Vault found = _repo.Get(id);
    //     if (found == null)
    //     {
    //         throw new Exception("Invalid ID");
    //     }
    //     return found;
    // }

    internal Vault GetOne(int id, string userId)
    {
        Vault vault = _repo.GetOne(id);
        if (vault == null) throw new Exception($"No vault at {id}.");
        if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception($"That vault is private.");
        return vault;
    }
    internal Vault Edit(Vault vaultData, string userId)
    {
        Vault original = GetOne(vaultData.Id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("You don't have access.");
        }
        {
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            original.Img = vaultData.Img ?? original.Img;
            original.IsPrivate = vaultData.IsPrivate != original.IsPrivate ? vaultData.IsPrivate : original.IsPrivate;
            _repo.Edit(original);
            return original;
        }
    }

    internal void Delete(int id, string userId)
    {
        Vault original = GetOne(id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("O' you don't have the right!");
        }
        _repo.Delete(id);
    }



    internal List<KeepVaultVueModel> GetVaultKeepsByVaultId(int id, string userId)

    {
        Vault found = GetOne(id, userId);
        if (found.IsPrivate == true && found.CreatorId != userId)
        {
            throw new Exception("You do not have access");
        }
        List<KeepVaultVueModel> vaultKeeps = _repo.GetVaultKeepsByVaultId(id);
        return vaultKeeps;
    }

    internal List<Vault> GetVaultsByCreatorId(string profileId, string userInfo)
    {
        List<Vault> foundVaults = _repo.GetVaultsByCreatorId(profileId);
        Account foundAccount = _aserv.GetAccountById(profileId);
        if (foundAccount.Id != userInfo)
        {
            foundVaults = foundVaults.FindAll(v => v.IsPrivate == false);
            return foundVaults;
        }

        return foundVaults;

    }
}
