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
        keepData.CreatedAt = DateTime.UtcNow;
        keepData.UpdatedAt = DateTime.UtcNow;
        DataColumn.Kept = 0;
        return keepData;
    }
}
