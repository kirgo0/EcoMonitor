using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class EnvFactorUpdateDTO
    {
        [Required]
        [Range(0,int.MaxValue)]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public double factor_value { get; set; }
        [Required]
        public int passport_id { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_Ca_value { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_Ch_value { get; set; }
        [Required]
        public int rfc_factor_id { get; set; }

    }
}
