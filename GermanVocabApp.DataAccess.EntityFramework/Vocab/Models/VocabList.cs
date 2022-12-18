using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.Shared.Abstractions;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab.Models
{
    public class VocabList : EntityBase, ISoftDeletable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual IEnumerable<VocabListItem> ListItems { get; set; }
    }
}
