using EcoMonitor.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    [Index(nameof(factor_Name), nameof(passport_id), IsUnique = true)]
    public class EnvFactor
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_value { get; set; }
        [Range(0, double.MaxValue)]
        public double factor_Ca_value { get; set; }
        [Range(0, double.MaxValue)]
        public double factor_Ch_value { get; set; }
        [Required]
        [ForeignKey("Passport")]
        public int passport_id { get; set; }

        public Passport Passport { get; set; }

        //[Required]
        [ForeignKey("RfcFactor")]
        public int rfc_factor_id { get; set; }

        public RfcFactor RfcFactor { get; set; }
    }
}
