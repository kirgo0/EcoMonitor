using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateService.Tax
{
    public class TaxWaterDTO
    {
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Ca { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double WaterTaxNorm { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Kf { get; set; }
    }
}
