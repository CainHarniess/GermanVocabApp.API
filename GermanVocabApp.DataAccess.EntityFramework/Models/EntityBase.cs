namespace GermanVocabApp.DataAccess.EntityFramework.Models
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
