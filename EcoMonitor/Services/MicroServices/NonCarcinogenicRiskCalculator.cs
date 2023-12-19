using EcoMonitor.Model.DTO.CalculateServiceDTO;

namespace EcoMonitor.Services.MicroServices
{
    public class NonCarcinogenicRiskCalculator
    {
        public double Calculate(NonCarcinogenicRiskDTO values)
        {
            return values.C / values.Rfc;
        }
    }
}
