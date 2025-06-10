namespace egorDipl.Data.Models
{
    public class TagsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tags> Tags { get; set; }  // Добавлено virtual
    }
}
