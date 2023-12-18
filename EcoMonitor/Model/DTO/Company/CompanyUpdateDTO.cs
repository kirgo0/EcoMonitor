using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Company
{
    public class CompanyUpdateDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int id { get; set; }
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        [Required]
        public int city_id { get; set; }
    }
}
