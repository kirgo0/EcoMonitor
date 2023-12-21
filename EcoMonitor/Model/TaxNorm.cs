using EcoMonitor.Migrations;
using EcoMonitor.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    [Index(nameof(year), nameof(name), IsUnique = true)]
    public class TaxNorm : IEntityWithId
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string name { get; set; }
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

        [ForeignKey("Pollutant")]
        public int? pollutant_id { get; set; }

        public Pollutant Pollutant { get; set; }

        [Column(TypeName = "YEAR(4)")]
        public int year { get; set; }
    }
}
