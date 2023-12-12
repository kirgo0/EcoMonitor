using EcoMonitor.Model.DTO.CalculateServiceDTO;

namespace EcoMonitor.Services.Calculator
{
    public class NonCarcinogenicRiskCalculator
    {
        public double CalculateRisk(NonCarcinogenicRiskDTO values)
        {
            return values.C / values.Rfc;
        }
    }
}
