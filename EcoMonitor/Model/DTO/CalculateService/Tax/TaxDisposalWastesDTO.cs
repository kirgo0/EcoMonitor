using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateService.Tax
{
    public class TaxDisposalWastesDTO
    {
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Ca { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double WasteDisposalTaxNorm { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Kt { get; set; }
        [Required]
        [Range(1,3)]
        public double Ko { get; set; }
    }
}
