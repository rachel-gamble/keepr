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


    internal Vault Edit(Vault original)
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
        return original;
    }

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

    internal List<KeepVaultVueModel> GetVaultKeepsByVaultId(int id)
    {
        string sql = @"
             SELECT
                k.*,
                vk.id AS vaultKeepId,
                a.*
            FROM vaultkeeps vk
            JOIN keeps k ON vk.keepId = k.id
            JOIN accounts a ON k.creatorId = a.id
            WHERE vk.vaultId = @id;
            ";
        List<KeepVaultVueModel> Keeps = _db.Query<KeepVaultVueModel, Profile, KeepVaultVueModel>(sql, (vk, account) =>
        {
            vk.Creator = account;
            return vk;
        }, new { id }).ToList();
        return Keeps;
    }
}

