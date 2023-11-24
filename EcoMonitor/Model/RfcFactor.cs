using EcoMonitor.Migrations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model
{
    [Index(nameof(factor_Name), IsUnique = true)]
    public class RfcFactor
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_value { get; set; }
    }
}
