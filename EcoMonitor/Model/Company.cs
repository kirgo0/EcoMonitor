using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcoMonitor.Model
{
    [Index(nameof(name), IsUnique = true)]
    public class Company
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(45)]
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
    }
}
