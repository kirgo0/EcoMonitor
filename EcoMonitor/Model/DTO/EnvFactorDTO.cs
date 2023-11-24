using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class EnvFactorDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string factor_Name { get; set; }
        [Required]
        public double factor_value { get; set; }
        [Required]
        public int passport_id { get; set; }
        [Range(0, double.MaxValue)]
        public double factor_Ca_value { get; set; }
        [Range(0, double.MaxValue)]
        public double factor_Ch_value { get; set; }
        [Range(0, double.MaxValue)]
        public int rfc_factor_id { get; set; }

    }
}
