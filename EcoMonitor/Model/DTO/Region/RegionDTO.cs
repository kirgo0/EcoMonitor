using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.Region
{
    public class RegionDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        [Required]
        public double square { get; set; }
    }
}
