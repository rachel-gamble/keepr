namespace keepr.Services;

public class VaultsService
{
    public readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    public Vault Create(Vault vaultData, string userId)
    {
        return _repo.Create(vaultData, userId);
    }

    internal List<Vault> GetByCreatorId(string profileId)
    {
        return _repo.GetVaultsByCreatorId(profileId);
    }


    internal Vault Get(int id, string userId)
    {
        Vault found = _repo.Get(id);
        if (found == null)
        {
            throw new Exception("Invalid ID");
        }
        return found;
    }

    internal Vault Edit(Vault vaultData, string userId)
    {
        Vault original = Get(vaultData.Id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("You don't have access.");
        }
        {

            original.Description = vaultData.Description ?? original.Description;
            original.Img = vaultData.Img ?? original.Img;
            original.IsPrivate = vaultData.IsPrivate != original.IsPrivate ? vaultData.IsPrivate : original.IsPrivate;
            _repo.Edit(original);
            return original;
        }
    }

    internal void Delete(int id, string userId)
    {
        Vault original = Get(id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("O' you don't have the right!");
        }
        _repo.Delete(id);
    }
}
