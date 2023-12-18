using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.RfcFactor
{
    public class RfcFactorDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
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

        [Required]
        [MaxLength(150)]
        public string damaged_organs { get; set; }
    }
}
