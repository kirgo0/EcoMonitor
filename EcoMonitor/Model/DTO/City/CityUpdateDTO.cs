using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.City
{
    public class CityUpdateDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        [Required]
        public bool isResort { get; set; }
        [Required]
        public int region_id { get; set; }
    }
}
