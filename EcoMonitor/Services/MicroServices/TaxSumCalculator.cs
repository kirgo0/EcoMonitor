using EcoMonitor.Model.DTO.CalculateService;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Services.MicroServices
{
    public class TaxSumCalculator
    {
        private readonly ITaxNormRepository _taxNormRepository;
        private readonly IRfcFactorRepository _rfcFactorRepository;
        private readonly IEnvFactorRepository _envFactorRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPassportRepository _passportRepository;

        public TaxSumCalculator(
            ITaxNormRepository taxNormRepository, 
            IRfcFactorRepository rfcFactorRepository, 
            IEnvFactorRepository envFactorRepository, 
            ICompanyRepository companyRepository, 
            IPassportRepository passportRepository
            )
        {
            _taxNormRepository = taxNormRepository;
            _rfcFactorRepository = rfcFactorRepository;
            _envFactorRepository = envFactorRepository;
            _companyRepository = companyRepository;
            _passportRepository = passportRepository;
        }

        public List<double> Calculate(TaxesDTO dto)
        {
            return null;
        }

        private double CalculateAirTax()
        {
            throw new NotImplementedException();
        }

        private double CalculateWaterTax()
        {
            throw new NotImplementedException();
        }

        private double CalculateDisposeWastesTax()
        {
            throw new NotImplementedException();
        }

        private double CalculateRadioactiveTax()
        {
            throw new NotImplementedException();
        }

        private double CalculateTempRadioactiveTax()
        {
            throw new NotImplementedException();
        }
    }
}
