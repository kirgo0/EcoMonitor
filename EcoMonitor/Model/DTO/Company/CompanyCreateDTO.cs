using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Company
{
    public class CompanyCreateDTO
    {
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        [Required]
        public int city_id { get; set; }
    }
}
