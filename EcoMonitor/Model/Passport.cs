using EcoMonitor.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySql.Data.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EcoMonitor.Model
{
    [Index(nameof(year), nameof(company_id), IsUnique = true)]
    public class Passport : IEntityWithId
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Column(TypeName = "YEAR(4)")]
        public int year { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int company_id { get; set; }
        public Company Company { get; set; }
    }
}
