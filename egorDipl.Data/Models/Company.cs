﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace egorDipl.Data.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int CountEvents { get; set; }
        public int FeedBacksCount { get; set; }
        public int Rating { get; set; }
        public int IsOrganizer { get; set; }
        public int IsSponsor { get; set; }
        public string? Description { get; set; }
    }
}
