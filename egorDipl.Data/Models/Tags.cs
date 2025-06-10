namespace egorDipl.Data.Models
{
    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual TagsCategory Category { get; set; }       // Добавлено virtual
        public virtual ICollection<EventTag> EventTags { get; set; } // Добавлено virtual
    }
}
