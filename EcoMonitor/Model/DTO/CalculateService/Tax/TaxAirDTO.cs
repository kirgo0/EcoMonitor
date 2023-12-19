using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateService.Tax
{
    public class TaxAirDTO
    {
        [Required]
        public double Ca { get; set; }
        [Required]
        public double AirTaxNorm { get; set; }
    }
}
