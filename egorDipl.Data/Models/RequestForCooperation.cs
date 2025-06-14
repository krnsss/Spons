﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace egorDipl.Data.Models
{
    public class RequestForCooperation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? SenderCompanyId { get; set; }
        public virtual Company? SenderCompany { get; set; }
        public string? TextLetter { get; set; }
        public int? EventId { get; set; }
        public virtual Event? Event { get; set; }
        public int? StatusId { get; set; }
        public virtual RequestStatus? Status { get; set; }
    }
}
