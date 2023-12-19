using EcoMonitor.Model.DTO.CalculateService;

namespace EcoMonitor.Services.MicroServices
{
    public class CompensationCalculator
    {
        private const double Kzi = 1.0;
        public List<double> Calculate(CompensationDTO values)
        {
            var Kf = values.kf;
            var Kt = GetPopulationCoef(values.pop) * Kf;
            var A = values.gdk > 1 ? 10/values.gdk : 1/values.gdk;

            var convertedPollution = values.env_factor * 1_000_000 / 31_536_088;
            var convertedMass = values.mass_flow_rate * 1_000_000 / 31_536_088;

            if (convertedPollution - convertedMass <= 0) return new List<double>() { 0 };
            var Mi = 3.6 * Math.Pow(10, -3) * (convertedPollution - convertedMass) * values.time_hours;

            var compensation = Mi * 1.1 * values.min_salary * A * Kt * Kzi;

            return new List<double>() { compensation, values.mass_flow_rate, values.env_factor};
        }   

        private double GetPopulationCoef(double pop)
        {
            return pop switch
            {
                < 100000 => 1,
                < 250000 => 1.2,
                < 500000 => 1.35,
                < 1000000 => 1.55,
                _ => 1.8
            };
        }
    }
}
