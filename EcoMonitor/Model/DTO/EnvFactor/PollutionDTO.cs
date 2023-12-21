using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.EnvFactor
{
    public class PollutionDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double value { get; set; }
        [Required]
        public int passport_id { get; set; }
        [Range(0, double.MaxValue)]
        public double CA_value { get; set; }
        [Range(0, double.MaxValue)]
        public double CH_value { get; set; }
        [Range(0, int.MaxValue)]
        public int? pollutant_id { get; set; }

    }
}
