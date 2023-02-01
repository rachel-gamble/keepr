namespace keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
        string sql = @"
           INSERT INTO vaultkeeps
           (creatorId, vaultId, keepId)
           VALUES
           (@CreatorId, @VaultId, @KeepId);
           SELECT LAST_INSERT_ID();
           ";
        vaultKeepData.Id = _db.ExecuteScalar<int>(sql, vaultKeepData);
        return vaultKeepData;
    }

    internal VaultKeep Get(int id)
    {
        string sql = @"
            SELECT
            vk.*
            FROM vaultkeeps vk
            WHERE vk.id = @id;";
        return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
    internal List<KeepVaultVueModel> GetKeepsByVaultId(int vaultId)
    {
        var sql = @"
    SELECT
    vk.*,
    COUNT(k.id) AS Kept,
    vk.id AS VaultKeepId,
    vk.creatorId AS VaultKeepCreatorId,
    k.*,
    a.*
    FROM vaultKeeps vk
    JOIN accounts a ON a.id = vk.creatorId
    JOIN keeps k ON k.id = vk.keepId
    WHERE vk.vaultId = @vaultId
    GROUP BY vk.id
    ;";
        return _db.Query<KeepVaultVueModel, Profile, KeepVaultVueModel>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { vaultId }).ToList();
    }

    internal void Remove(int id)
    {
        string sql = @"
        DELETE 
        FROM vaultkeeps
        WHERE id = @id
        LIMIT 1;
        ";
        _db.Execute(sql, new { id });
    }
}
