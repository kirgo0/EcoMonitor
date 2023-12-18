using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Company
{
    public class CompanyDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        public int city_id { get; set; }
    }
}
