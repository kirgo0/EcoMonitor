using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class PassportCreateDTO
    {
        [Required]
        [MaxLength(45)]
        public string name { get; set; }

        [Required]
        [Range(2000,2030)]
        public int year { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public string data4 { get; set; }
        public string data5 { get; set; }

        [Required]
        public int company_id { get; set; }
    }
}
