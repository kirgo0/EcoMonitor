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
        private TaxSumCalculator _taxSumCalculator;
        public CalculateService(TaxSumCalculator taxSumCalculator)
        {
            _taxSumCalculator = taxSumCalculator;
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
            return _taxSumCalculator.Calculate(dto);
            //return new TaxSumCalculator().Calculate(dto);
        }
    }
}
