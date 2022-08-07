namespace GermanVocabApp.DataAccess.EntityFramework.Models
{
    public class VocabList : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<VocabListItem>? ListItems { get; set; }
    }
}
