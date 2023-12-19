using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model.DTO.City
{
    public class CityDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        [Required]
        public bool isResort { get; set; }
        public int region_id { get; set; }
    }
}
