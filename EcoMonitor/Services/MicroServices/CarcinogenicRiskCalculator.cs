using EcoMonitor.Model.DTO.CalculateServiceDTO;

namespace EcoMonitor.Services.MicroServices
{
    public class CarcinogenicRiskCalculator
    {
        public List<double> Calculate(CarcinogenicRiskDTO values)
        {
            var ladd = (values.Ca * values.Tout * values.Vout +
                values.Ch * values.Tin * values.Vin) *
                values.EF * values.ED /
                (values.BW * values.AT * 365);

            var Cr = ladd * values.SF;
            return new List<double> { Cr, Cr * values.POP};
        }
    }
}
