using GermanVocabApp.DataAccess.EntityFramework.Configuration;
using GermanVocabApp.DataAccess.EntityFramework.Models;
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
