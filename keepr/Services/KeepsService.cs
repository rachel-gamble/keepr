namespace keepr.Services;

public class KeepsService
{
    public readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    public Keep Create(Keep keepData)
    {
        return _repo.Create(keepData);
    }
}
