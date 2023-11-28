using EcoMonitor.Model.DTO;

namespace EcoMonitor.Calculator
{
    public class NonCarcinogenicRiskCalculator
    {
        public double CalculateRisk(NonCarcinogenicRiskDTO values)
        {
            return values.C / values.Rfc;
        }
    }
}
