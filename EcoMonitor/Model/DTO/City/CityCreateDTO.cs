using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.City
{
    public class CityCreateDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        [Required]
        public int region_id { get; set; }
    }
}
