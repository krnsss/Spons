using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace egorDipl.Data.Models
{
    public class Cooperation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? OrganizerId { get; set; }
        public virtual Company? Organizer { get; set; }
        public int? SponsorId { get; set; }
        public virtual Company? Sponsor { get; set; }
        public int? EventId { get; set; }
        public virtual Event? Event { get; set; }
        public int? RequestId { get; set; }
        public virtual RequestForCooperation? Request { get; set; }
    }
}
