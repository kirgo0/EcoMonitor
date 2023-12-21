using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.EnvFactor
{
    public class PollutionUpdateDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double value { get; set; }
        [Required]
        public int passport_id { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double CA_value { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double CH_value { get; set; }
        [Required]
        public int pollutant_id { get; set; }

    }
}
