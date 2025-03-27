using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using egorDipl.Data;
using Microsoft.AspNetCore.Identity;

namespace egorDipl.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? Post { get; set; }
        public int RoleId { get; set; }
        public StaffRole? Role { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        public int UniCode { get; set; }
        public string Password { get; set; }

        public static int GenerateUniCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
    }
}
