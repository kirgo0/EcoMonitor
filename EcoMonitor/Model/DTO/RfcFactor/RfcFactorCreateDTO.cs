﻿using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.RfcFactor
{
    public class RfcFactorCreateDTO
    {
        [Required]
        [MaxLength(150)]
        public string factor_Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double factor_value { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double SF_value { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double GDK_value { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double mass_flow_rate { get; set; }

        public string damaged_organs { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int tax_norm_id { get; set; }
    }
}