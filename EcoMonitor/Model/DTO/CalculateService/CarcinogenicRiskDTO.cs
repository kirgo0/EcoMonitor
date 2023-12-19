using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateServiceDTO
{
    public class CarcinogenicRiskDTO
    {
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Ca { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Ch { get; set; }
        [Required]
        [ExcludeZero(24)]
        public double Tout { get; set; }
        [Required]
        [ExcludeZero(24)]
        public double Tin { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Vout { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Vin { get; set; }
        [Required]
        [ExcludeZero(365)]
        public double EF { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double ED { get; set; }
        [Required]
        [ExcludeZero(300)]
        public double BW { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double AT { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double SF { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int POP { get; set; }


    }
}
