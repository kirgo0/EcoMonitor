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
    }

    public class CalculateService : ICalculateService
    {
        public CalculateService()
        {
        }

        public List<double> CalculateCarcinogenic(CarcinogenicRiskDTO dto)
        {
            return new CarcinogenicRiskCalculator().Calculate(dto);
        }

        public List<double> CalculateCompensation(CompensationDTO dto)
        {
            return new CompensationCalculator().Calculate(dto);
        }

        public double CalculateNonCarcinogenic(NonCarcinogenicRiskDTO dto)
        {
            return new NonCarcinogenicRiskCalculator().Calculate(dto);
        }
    }
}
