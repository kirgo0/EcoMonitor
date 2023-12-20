using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CompanyWaste
{
    public class CompanyWasteUpdateDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int passport_id { get; set; }
        [Required]
        public bool Koc { get; set; }
        [Required]
        public bool Ko { get; set; }
        [Required]
        public double Kt { get; set; }
    }
}
