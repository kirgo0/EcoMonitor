using EcoMonitor.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    [Index(nameof(name), nameof(passport_id), IsUnique = true)]
    public class Pollution
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double pollution_value { get; set; }
        [Range(0, double.MaxValue)]
        public double CA_value { get; set; }
        [Range(0, double.MaxValue)]
        public double CH_value { get; set; }
        [Required]
        [ForeignKey("Passport")]
        public int passport_id { get; set; }

        public Passport Passport { get; set; }

        [ForeignKey("Pollutant")]
        public int? pollutant_id { get; set; }
        public Pollutant Pollutant { get; set; }

        [Range(0, double.MaxValue)]
        public double? radioactive_volume { get; set; }

        [Range(0, double.MaxValue)]
        public double? radioactive_disposal_time { get; set; }
    }
}
