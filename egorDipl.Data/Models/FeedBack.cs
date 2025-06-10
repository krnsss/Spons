using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace egorDipl.Data.Models
{
    public class FeedBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? AboutCompanyId { get; set; }
        public virtual Company? AboutCompany { get; set; }
        public int StarsCount { get; set; }
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
