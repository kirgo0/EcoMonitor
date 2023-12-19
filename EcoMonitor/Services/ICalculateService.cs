using EcoMonitor.Model.DTO.CalculateService;
using EcoMonitor.Model.DTO.CalculateServiceDTO;
using EcoMonitor.Services.MicroServices;

namespace EcoMonitor.Services
{
    public interface ICalculateService
    {
        List<double> CalculateCarcinogenic(CarcinogenicRiskDTO dto);
        double CalculateNonCarcinogenic(NonCarcinogenicRiskDTO dto);
        List<double> CalculateCompensation(CompensationDTO dto);

        List<double> CalculateTaxes(TaxesDTO dto);
    }

    public class CalculateService : ICalculateService
    {
        public CalculateService()
        {
        }

        public List<double> CalculateCarcinogenic(CarcinogenicRiskDTO dto)
        {
            return CarcinogenicRiskCalculator.Calculate(dto);
        }

        public List<double> CalculateCompensation(CompensationDTO dto)
        {
            return CompensationCalculator.Calculate(dto);
        }
        public double CalculateNonCarcinogenic(NonCarcinogenicRiskDTO dto)
        {
            return NonCarcinogenicRiskCalculator.Calculate(dto);
        }

        public List<double> CalculateTaxes(TaxesDTO dto)
        {
            return null;
            //return new TaxSumCalculator().Calculate(dto);
        }
    }
}
