using egorDipl.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace egorDipl.Data.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        
        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        public string? Note { get; set; }

        public NotificationStatus NotificationStatus { get; set; }
    }
}
