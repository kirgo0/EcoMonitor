using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class CompanyCreateDTO
    {
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}
