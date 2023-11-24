using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
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
    }
}
