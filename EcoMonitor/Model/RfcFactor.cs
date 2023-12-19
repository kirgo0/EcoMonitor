using EcoMonitor.Migrations;
using EcoMonitor.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    [Index(nameof(factor_Name), IsUnique = true)]
    public class RfcFactor : IEntityWithId
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
        public double? SF_value { get; set; }

        [Range(0, double.MaxValue)]
        public double? GDK_value { get; set; }

        [Range(0, double.MaxValue)]
        public double? mass_flow_rate { get; set; }

        [MaxLength(150)]
        public string? damaged_organs { get; set; }

        [ForeignKey("TaxNorm")]
        public int? tax_norm_id { get; set; }

        public TaxNorm TaxNorm { get; set; }
    }
}
