﻿using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO
{
    public class CompanyUpdateDTO
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int id { get; set; }
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}
