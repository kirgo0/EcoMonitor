using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Passport
{
    public class PassportUpdateDTO
    {

        [Required]
        public int id { get; set; }

        [Required]
        [Range(2000, 2030)]
        public int year { get; set; }
        [Required]
        public double source_operating_time { get; set; }

        [Required]
        public int company_id { get; set; }
    }
}
