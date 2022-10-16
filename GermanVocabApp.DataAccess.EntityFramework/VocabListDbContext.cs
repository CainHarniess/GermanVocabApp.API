using GermanVocabApp.DataAccess.EntityFramework.Configuration;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
                if (entity.DeletedDate == null)
                {
                    entry.Entity.UpdatedDate = transactionTimeStamp;
                    continue;
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                if (entity.DeletedDate != null)
                {
                    continue;
                }
                entity.DeletedDate = transactionTimeStamp;
                entry.State = EntityState.Modified;
                continue;
            }
        }

        return await base.SaveChangesAsync(token);
    }
}
