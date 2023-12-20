using EcoMonitor.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Model
{
    public class CompanyWaste : IEntityWithId
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("Passport")]
        public int passport_id { get; set; }
        public Passport Passport { get; set; }
        [Required]
        public bool Koc { get; set; }
        [Required]
        public bool Ko { get; set; }
        [Required]
        public double Kt { get; set; }
    }
}
