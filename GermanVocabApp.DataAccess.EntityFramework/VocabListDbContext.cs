using GermanVocabApp.DataAccess.EntityFramework.Configuration;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework;
public class VocabListDbContext : DbContext
{
    //public const string ConnectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=GermanVocabApp";

    public VocabListDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<VocabList> VocablLists { get; set; }
    public DbSet<VocabListItem> VocablListItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureConventions();
        modelBuilder.Configure();
    }
}
