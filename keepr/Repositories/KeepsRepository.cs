namespace keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep Create(Keep keepData)
    {
        string sql = @"
    INSERT
    INTO keeps
    (creatorId, name, description, img)
    VALUES
    (@CreatorId, @Name, @Description, @Img);
    SELECT LAST_INSERT_ID();
    ";

        keepData.Id = _db.ExecuteScalar<int>(sql, keepData);
        //extra
        // keepData.CreatedAt = DateTime.UtcNow;
        // keepData.UpdatedAt = DateTime.UtcNow;
        keepData.Kept = 0;
        return keepData;
    }

    internal List<Keep> Get()
    {
        string sql = @"
            SELECT
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            ";
        return _db.Query<Profile, Keep, Keep>(sql, (a, k) =>
        {
            k.Creator = a;
            return k;
        }).ToList();
    }

    internal List<Keep> GetByCreatorId(string id)
    {
        string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE creatorId = @id";
        return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
            k.Creator = p;
            return k;
        }, new { id }).ToList();
    }
    internal Keep Get(int id)
    {
        string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id";
        return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
            k.Creator = p;
            return k;
        }, new { id }).FirstOrDefault();
    }

    internal void Edit(Keep original)
    {
        string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @Views,
            kept = @Kept
            WHERE id = @Id;
            ";
        _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
        string sql = @"DELETE FROM keeps WHERE id = @id LIMIT 1;";
        _db.Execute(sql, new { id });
    }


}
