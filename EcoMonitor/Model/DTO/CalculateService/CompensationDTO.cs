using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateService
{
    public class CompensationDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int pop { get; set; }

        [Required]
        [ExcludeZero(double.MaxValue)]
        public double gdk { get; set; }

        [Required]
        [ExcludeZero(double.MaxValue)]
        public double env_factor { get; set; }

        [Required]
        [ExcludeZero(double.MaxValue)]
        public double mass_flow_rate { get; set; }

        [Required]
        [Range(1,8760)]
        public int time_hours { get; set; }

        [Required]
        [ExcludeZero(double.MaxValue)]
        public double min_salary { get; set; }

        [Required]
        [ExcludeZero(double.MaxValue)]
        public double kf { get; set; }
    }
}
