using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class EnvFactorDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string factor_Name { get; set; }
        [Required]
        public double factor_value { get; set; }
        [Required]
        public int passport_id { get; set; }

    }
}
