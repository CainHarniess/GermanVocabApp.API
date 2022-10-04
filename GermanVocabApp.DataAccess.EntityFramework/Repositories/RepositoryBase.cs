namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public abstract class RepositoryBase
{
    private readonly VocabListDbContext _context;

    public RepositoryBase(VocabListDbContext context)
    {
        _context = context;
    }

    protected VocabListDbContext Context => _context;
}
