namespace keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault Create(Vault vaultData, string userId)
    {
        string sql = @"
            INSERT INTO vaults
            (name, description, img, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @Img, @isPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();";
        vaultData.Id = _db.ExecuteScalar<int>(sql, vaultData);
        return vaultData;
    }

    internal Vault Get(int id)
    {
        string sql = @"
            SELECT 
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id";
        return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
            v.Creator = p;
            return v;
        }, new { id }).FirstOrDefault();
    }

    internal void Edit(Vault original)
    {
        string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            isPrivate = @IsPrivate
            WHERE id = @Id;";
        _db.Execute(sql, original);
    }

    // internal List<VaultKeepViewModel> GetVaultKeepViewModelFromVault(int id)
    // {
    //     //FIXME I need to get keeps via VaultId utilizing the VaultKeep Object relationship.
    //     //NOTE The "vk.id AS vaultKeepId" MUST come after k.* and before a.*
    //     string sql = @"
    //         SELECT 
    //             k.*,
    //             vk.id AS vaultKeepId,
    //             a.*
    //         FROM vaultkeeps vk
    //         JOIN keeps k ON vk.keepId = k.id
    //         JOIN accounts a ON k.creatorId = a.id
    //         WHERE vk.vaultId = @id";
    //     return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (k, p) =>
    //     {
    //         k.Creator = p;
    //         return k;
    //     }, new { id }).ToList();
    // }

    internal void Delete(int id)
    {
        string sql = @"DELETE FROM vaults WHERE id = @id LIMIT 1;";
        _db.Execute(sql, new { id });
    }

    internal List<Vault> GetMyVaults(string id)
    {
        string sql = @"
            SELECT
            v.*
            FROM vaults v
            WHERE v.creatorId = @id;
            ";
        return _db.Query<Vault>(sql, new { id }).ToList();
    }

    internal List<Vault> GetVaultsByCreatorId(string id)
    {
        string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id;";
        return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
            v.Creator = p;
            return v;
        }, new { id }).ToList();
    }

    internal List<VaultKeep> GetVaultKeepsById(int id)
    {
        string sql = @"
            SELECT
            vk.*
            FROM keeps vk
            WHERE vk.id = @id
            ";
        return _db.Query<VaultKeep>(sql, new { id }).ToList();
    }
}
