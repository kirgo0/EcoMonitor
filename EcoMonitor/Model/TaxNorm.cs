using EcoMonitor.Migrations;
using EcoMonitor.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    [Index(nameof(year), nameof(factor_Name), IsUnique = true)]
    public class TaxNorm : IEntityWithId
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double air_emissions { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double water_emissions { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double disposal_of_wastes { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double radioactive_wastes { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double temporary_disposal_of_radioactive_wastes { get; set; }

        [ForeignKey("RfcFactor")]
        public int? rfc_factor_id { get; set; }

        public RfcFactor RfcFactor { get; set; }

        [Column(TypeName = "YEAR(4)")]
        public int? year { get; set; }
    }
}
