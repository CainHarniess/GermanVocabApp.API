using GermanVocabApp.DataAccess.EntityFramework.Configuration;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Repositories;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework;
public class VocabListDbContext : DbContext
{
    public VocabListDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<VocabList> VocablLists { get; set; }
    public DbSet<VocabListItem> VocablListItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDatabaseConventions();
        modelBuilder.ConfigureEntities();
    }
}

//public class VocabUnitOfWorkAsync
//{
//    private VocabListDbContext _context;
//    private VocabListRepositoryAsync _listRepository;
//    private ItemRepositoryAsync _itemRepository;

//    public VocabUnitOfWork(VocabListDbContext context, VocabListRepositoryAsync listRepository,
//                           ItemRepositoryAsync itemRepository)
//    {
//        _context = context;
//        _listRepository = listRepository;
//        _itemRepository = itemRepository;
//    }

//    public async Task SaveChangesAsync()
//    {
//        await _context.SaveChangesAsync();
//    }
//}
