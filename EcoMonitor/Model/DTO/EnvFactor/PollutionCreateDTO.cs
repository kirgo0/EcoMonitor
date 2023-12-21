using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.EnvFactor
{
    public class PollutionCreateDTO
    {
        [Required]
        [MaxLength(150)]
        public string name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double value { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int passport_id { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double CA_value { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double CH_value { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int pollutant_id { get; set; }
    }
}
