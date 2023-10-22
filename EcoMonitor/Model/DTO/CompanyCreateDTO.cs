using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class CompanyCreateDTO
    {
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        public string data1 { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
    }
}
