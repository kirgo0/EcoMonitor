using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Region
{
    public class RegionCreateDTO
    { 
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        public double? square { get; set; }
    }
}
