using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace egorDipl.Data.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? OrganizerId { get; set; }

        public virtual Company? Organizer { get; set; }

        public int? SponsorId { get; set; }

        public virtual Company? Sponsor { get; set; }

        public int? StatusId { get; set; }

        public virtual EventStatus? Status { get; set; }

        public DateTime Date { get; set; }
    }
}
