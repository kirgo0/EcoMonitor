using EcoMonitor.Model;
using EcoMonitor.Model.DTO.CalculateService;
using EcoMonitor.Repository.IRepository;

namespace EcoMonitor.Services.MicroServices
{
    public class TaxSumCalculator
    {

        private readonly ITaxNormRepository _taxNormRepository;
        private readonly ICompanyWasteRepository _companyWasteRepository;
        private readonly IPollutionRepository _envFactorRepository;

        public TaxSumCalculator(ITaxNormRepository taxNormRepository, ICompanyWasteRepository companyWasteRepository, IPollutionRepository envFactorRepository)
        {
            _taxNormRepository = taxNormRepository;
            _companyWasteRepository = companyWasteRepository;
            _envFactorRepository = envFactorRepository;
        }

        public List<double> Calculate(TaxesDTO dto)
        {
            var air_tax = CalculateAirTax(dto);
            var water_tax = CalculateWaterTax(dto);
            var dispose_wastes_tax = CalculateDisposeWastesTax(dto);
            var radioactive_tax = CalculateRadioactiveTax(dto);
            var temp_rad_tax = CalculateTempRadioactiveTax(dto);

            return new List<double>() { air_tax, water_tax, dispose_wastes_tax, radioactive_tax, temp_rad_tax};
        }

        private double CalculateAirTax(TaxesDTO dto)
        {
            var masses = new List<Pollution>();
            var taxes = new List<TaxNorm>();

            GetEnvFactorsAndTaxes(dto, out masses, out taxes, dto.year);

            if (taxes.Count == 0) return 0.0;

            double result = 0;
            
            for (var i = 0; i < masses.Count; i++)
            {
                result += masses[i].value * taxes[i].air_emissions;
            }

            return result;
        }

        private double CalculateWaterTax(TaxesDTO dto)
        {
            var masses = new List<Pollution>();
            var taxes = new List<TaxNorm>();

            GetEnvFactorsAndTaxes(dto, out masses, out taxes, dto.year);

            if (taxes.Count == 0) return 0.0;

            double result = 0;

            for (var i = 0; i < masses.Count; i++)
            {
                var companyWaste = _companyWasteRepository.GetAsync(c => c.passport_id == masses[i].passport_id).Result;
                var koc = 1.0;
                if (companyWaste != null && companyWaste.Koc) koc = 1.5;
                result += masses[i].value * taxes[i].water_emissions * koc;
            }

            return result;
        }

        private double CalculateDisposeWastesTax(TaxesDTO dto)
        {
            var masses = new List<Pollution>();
            var taxes = new List<TaxNorm>();

            GetEnvFactorsAndTaxes(dto, out masses, out taxes, dto.year);

            if (taxes.Count == 0) return 0.0;

            double result = 0;

            for (var i = 0; i < masses.Count; i++)
            {
                var companyWaste = _companyWasteRepository.GetAsync(c => c.passport_id == masses[i].passport_id).Result;
                var kt = 1.0;
                var ko = 1.0;
                if (companyWaste != null && companyWaste.Ko)
                {
                    ko = 3;
                    kt = companyWaste.Kt;
                }
                result += masses[i].value * taxes[i].disposal_of_wastes * kt * ko;
            }

            return result;
        }

        private double CalculateRadioactiveTax(TaxesDTO dto)
        {
            var masses = new List<Pollution>();
            var taxes = new List<TaxNorm>();

            GetEnvFactorsAndTaxes(dto, out masses, out taxes, dto.year);

            if (taxes.Count == 0) return 0.0;

            double result = 0;

            for (var i = 0; i < masses.Count; i++)
            {
                var radVol = masses[i].radioactive_volume;
                if (radVol.HasValue && radVol.Value > 0)
                    result += masses[i].value * taxes[i].radioactive_wastes * radVol.Value * GetRadK();
                Console.WriteLine(result);
            }

            return result;
        }

        private double CalculateTempRadioactiveTax(TaxesDTO dto)
        {
            var masses = new List<Pollution>();
            var taxes = new List<TaxNorm>();

            GetEnvFactorsAndTaxes(dto, out masses, out taxes, dto.year);

            if (taxes.Count == 0) return 0.0;

            double result = 0;

            for (var i = 0; i < masses.Count; i++)
            {
                var time = masses[i].radioactive_disposal_time;
                var radVol = masses[i].radioactive_volume;

                if(time.HasValue && radVol.HasValue &&
                    time.Value>0 && radVol.Value>0)
                result += masses[i].value * 
                    taxes[i].temporary_disposal_of_radioactive_wastes *
                    time.Value * radVol.Value; 
            }

            return result;
        }

        private void GetEnvFactorsAndTaxes(TaxesDTO dto, out List<Pollution> masses, out List<TaxNorm> taxes, int year)
        {
            masses = new List<Pollution>();
            taxes = new List<TaxNorm>();

            masses = _envFactorRepository.GetAllAsync(f =>
                f.passport_id == dto.passport_id
                , includeProperties: "Passport").Result;

            if (masses.Count == 0)
            {
                return;
            }

            for (var i = 0; i < masses.Count; i++)
            {
                var f = masses[i];
                var tax = _taxNormRepository.GetAsync(t =>
                    t.pollutant_id == f.pollutant_id && t.year == year
                    ).Result;

                if (tax == null)
                {
                    masses.Remove(f);
                    i--;
                }
                else
                {
                    taxes.Add(tax);
                }

            }
        }
        private double GetRadK()
        {
            return (1 + 1 / 32 * 1.5);
        }
    }
}
