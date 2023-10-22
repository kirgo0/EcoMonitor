using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    public class EnvFactor
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_value { get; set; }
        [Required]
        [ForeignKey("Passport")]
        public int passport_id { get; set; }

        public Passport Passport { get; set; }
    }
}
