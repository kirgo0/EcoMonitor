﻿using EcoMonitor.Model.DTO;

namespace EcoMonitor.Calculator
{
    public class CarcinogenicRiskCalculator
    { 
        public double CalculateRisk(CarcinogenicRiskDTO values)
        {
            return ((values.Ca * values.Tout * values.Vout) + 
                (values.Ch * values.Tin * values.Vin)) * 
                values.EF * values.ED / 
                (values.BW * values.AT * 365);
        }
    }
}
