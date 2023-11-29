using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class RfcFactorCreateDTO
    {
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_value { get; set; }
        public string? damaged_organs { get; set; }
    }
}
