﻿using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.TaxNorm
{
    public class TaxNormUpdateDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double air_emissions { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double water_emissions { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double disposal_of_wastes { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double radioactive_wastes { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double temporary_disposal_of_radioactive_wastes { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int pollutant_id { get; set; }
        [Range(2000, 2030)]
        public int? year { get; set; }
    }
}
