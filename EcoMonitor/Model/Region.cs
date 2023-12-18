using EcoMonitor.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model
{
    [Index(nameof(name), IsUnique = true)]
    public class Region : IEntityWithId
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int population { get; set; }
        [Required]
        public double square { get; set; }
        public List<News> news { get; set; }
    }
}
