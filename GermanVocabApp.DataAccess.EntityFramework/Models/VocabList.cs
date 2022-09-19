using GermanVocabApp.DataAccess.Shared.Abstractions;

namespace GermanVocabApp.DataAccess.EntityFramework.Models
{
    public class VocabList : EntityBase, ISoftDeletable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual IEnumerable<VocabListItem> ListItems { get; set; }
    }
}
