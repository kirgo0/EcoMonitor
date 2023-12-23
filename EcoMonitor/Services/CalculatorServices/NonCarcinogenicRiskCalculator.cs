using EcoMonitor.Model.DTO.CalculateServiceDTO;

namespace EcoMonitor.Services.MicroServices
{
    public class NonCarcinogenicRiskCalculator
    {
        public static double Calculate(NonCarcinogenicRiskDTO values)
        {
            return values.C / values.Rfc;
        }
    }
}
