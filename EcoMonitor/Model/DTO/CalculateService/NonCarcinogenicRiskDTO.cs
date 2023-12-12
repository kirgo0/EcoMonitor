using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.CalculateServiceDTO
{
    public class NonCarcinogenicRiskDTO
    {
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double C { get; set; }
        [Required]
        [ExcludeZero(double.MaxValue)]
        public double Rfc { get; set; }
    }
}
