namespace keepr.Services;

public class KeepsService
{
    public readonly KeepsRepository _repo;
    public readonly VaultsService _vs;

    public KeepsService(KeepsRepository repo, VaultsService vs)
    {
        _repo = repo;
        _vs = vs;
    }

    public Keep Create(Keep keepData)
    {
        return _repo.Create(keepData);
    }

    internal List<Keep> GetByCreatorId(string profileId)
    {
        return _repo.GetByCreatorId(profileId);
    }

    internal List<Keep> Get()
    {
        return _repo.Get();
    }

    internal Keep GetKeepById(int id)
    {
        Keep found = _repo.GetKeepById(id);
        if (found == null)
        {
            throw new Exception("Invalid ID");
        }
        return found;
    }

    internal Keep Edit(Keep keepData, string userId)
    {
        Keep original = GetKeepById(keepData.Id);
        if (original.CreatorId != userId)
        {
            if (keepData.Name != original.Name && keepData.Description != original.Description && keepData.Img != original.Img)
            { throw new Exception("These properties are forbidden to edit at this time."); }
            original.Views = keepData.Views > 0 ? keepData.Views : original.Views;
            original.Kept = keepData.Kept > 0 ? keepData.Kept : original.Kept;
            _repo.Edit(original);
            return original;
        }
        else if (original.CreatorId == userId)
        {
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;
            original.Views = keepData.Views > 0 ? keepData.Views : original.Views;
            original.Kept = keepData.Kept > 0 ? keepData.Kept : original.Kept;
            _repo.Edit(original);
            return original;
        }
        else { throw new Exception("error editing this keep"); }
    }

    internal void Delete(int id, string userId)
    {
        Keep original = GetKeepById(id);
        if (original.CreatorId != userId)
        {
            throw new Exception("You do not own this keep");
        }
        _repo.Delete(id);
    }

    // get keeps by vault id
    // internal List<Keep> GetKeepsByVaultId(int vaultId, string userId)
    // {
    //     Vault vault = _vs.GetOne(vaultId, userId);
    //     List<Keep> keeps = _vs.GetVaultKeepsByVaultId(vaultId);
    //     return keeps;
    // }

    // internal <VaultKeep> GetVaultKeepsById(int Id){
    //     VaultKeep vaultKeep = _vs;
    // }
}
