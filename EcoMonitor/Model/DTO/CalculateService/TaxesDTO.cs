using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateService
{
    public class TaxesDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int company_id { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public int passport_id { get; set; }
        [Required]
        [Range(2000,2030)]
        public int year { get; set; }
    }
}
