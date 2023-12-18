using EcoMonitor.Migrations;
using EcoMonitor.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    [Index(nameof(name), IsUnique = true)]
    public class City : IEntityWithId
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        [Required]
        [ForeignKey("Region")]
        public int region_id { get; set; }
        public Region Region { get; set; }
    }
}
