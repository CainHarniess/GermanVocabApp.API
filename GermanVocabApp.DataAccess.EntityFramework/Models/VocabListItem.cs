namespace GermanVocabApp.DataAccess.EntityFramework.Models
{
    public class VocabListItem : EntityBase
    {
        public string Term { get; set; }

        public string WordType {  get; set; }
        
        public string Translation { get; set; }

        public virtual VocabList VocabList { get; set; }
    }
}
