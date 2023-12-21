using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.RfcFactor
{
    public class PollutantUpdateDTO
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
        [Range(0, double.MaxValue)]
        public double SF_value { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double GDK_value { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double mass_flow_rate { get; set; }
        public string damaged_organs { get; set; }

    }
}
