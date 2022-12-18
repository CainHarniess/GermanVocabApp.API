using GermanVocabApp.DataAccess.EntityFramework.Authentication.Models;
using GermanVocabApp.DataAccess.EntityFramework.Configuration;
using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GermanVocabApp.DataAccess.EntityFramework;
public class VocabListDbContext : DbContext
{
    public VocabListDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<VocabList> Lists { get; set; }
    public DbSet<VocabListItem> ListItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureDatabaseConventions();
        modelBuilder.ConfigureEntities();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken token = new())
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;

        IEnumerable<EntityEntry<EntityBase>> changeSet = ChangeTracker.Entries<EntityBase>();

        foreach (EntityEntry<EntityBase> entry in changeSet)
        {
            if (entry.State == EntityState.Unchanged)
            {
                continue;
            }

            EntityBase entity = entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = transactionTimeStamp;
                continue;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entity.DeletedDate.HasValue == false)
                {
                    entry.Entity.UpdatedDate = transactionTimeStamp;
                    continue;
                }
                entity.DeletedDate = transactionTimeStamp;
            }
        }

        return await base.SaveChangesAsync(token);
    }
}
