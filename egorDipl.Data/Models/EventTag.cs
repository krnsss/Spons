namespace egorDipl.Data.Models
{
    public class EventTag
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }  // Добавлено virtual
        public int TagId { get; set; }
        public virtual Tags Tag { get; set; }      // Добавлено virtual
    }
}
