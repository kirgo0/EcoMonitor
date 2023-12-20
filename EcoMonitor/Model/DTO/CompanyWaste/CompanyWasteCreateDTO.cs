using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CompanyWaste
{
    public class CompanyWasteCreateDTO
    {
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
